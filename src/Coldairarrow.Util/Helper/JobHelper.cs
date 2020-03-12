using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    public static class JobHelper
    {
        #region 私有成员

        private static IScheduler __scheduler;
        private static object _lock = new object();
        private static IScheduler _scheduler
        {
            get
            {
                if (__scheduler == null)
                {
                    lock (_lock)
                    {
                        if (__scheduler == null)
                        {
                            __scheduler = AsyncHelper.RunSync(() => StdSchedulerFactory.GetDefaultScheduler());
                            AsyncHelper.RunSync(() => __scheduler.Start());
                        }
                    }
                }

                return __scheduler;
            }
        }
        static ConcurrentDictionary<string, Action> _jobs { get; }
            = new ConcurrentDictionary<string, Action>();

        #endregion

        #region 外部接口

        /// <summary>
        /// 设置一个时间间隔的循环操作
        /// </summary>
        /// <param name="action">执行的操作</param>
        /// <param name="timeSpan">时间间隔</param>
        /// <returns>任务标识Id</returns>
        public static string SetIntervalJob(Action action, TimeSpan timeSpan)
        {
            string key = Guid.NewGuid().ToString();
            _jobs[key] = action;
            IJobDetail job = JobBuilder.Create<Job>()
               .WithIdentity(key)
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(key)
                .StartNow()
                .WithSimpleSchedule(x => x.WithInterval(timeSpan).RepeatForever())
                .Build();
            AsyncHelper.RunSync(() => _scheduler.ScheduleJob(job, trigger));

            return key;
        }

        /// <summary>
        /// 设置每天定时任务
        /// </summary>
        /// <param name="action">执行的任务</param>
        /// <param name="h">时</param>
        /// <param name="m">分</param>
        /// <param name="s">秒</param>
        /// <returns>任务标识Id</returns>
        public static string SetDailyJob(Action action, int h, int m, int s)
        {
            string key = Guid.NewGuid().ToString();
            _jobs[key] = action;
            IJobDetail job = JobBuilder.Create<Job>()
               .WithIdentity(key)
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(key)
                .StartNow()
                .WithCronSchedule($"{s} {m} {h} * * ?")//每天定时
                .Build();
            AsyncHelper.RunSync(() => _scheduler.ScheduleJob(job, trigger));

            return key;
        }

        /// <summary>
        /// 设置延时任务,仅执行一次
        /// </summary>
        /// <param name="action">执行的操作</param>
        /// <param name="delay">延时时间</param>
        /// <returns>任务标识Id</returns>
        public static string SetDelayJob(Action action, TimeSpan delay)
        {
            string key = Guid.NewGuid().ToString();
            action += () =>
            {
                RemoveJob(key);
            };
            _jobs[key] = action;

            IJobDetail job = JobBuilder.Create<Job>()
               .WithIdentity(key)
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(key)
                .StartAt(DateTime.Now + delay)
                .WithSimpleSchedule(x => x.WithRepeatCount(0).WithInterval(TimeSpan.FromSeconds(10)))
                .Build();
            AsyncHelper.RunSync(() => _scheduler.ScheduleJob(job, trigger));

            return key;
        }

        /// <summary>
        /// 通过表达式创建任务
        /// 表达式规则参考:http://www.jsons.cn/quartzcron/
        /// </summary>
        /// <param name="action">执行的操作</param>
        /// <param name="cronExpression">表达式</param>
        /// <returns></returns>
        public static string SetCronJob(Action action, string cronExpression)
        {
            string key = Guid.NewGuid().ToString();
            _jobs[key] = action;
            IJobDetail job = JobBuilder.Create<Job>()
               .WithIdentity(key)
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(key)
                .StartNow()
                .WithCronSchedule(cronExpression)
                .Build();
            AsyncHelper.RunSync(() => _scheduler.ScheduleJob(job, trigger));

            return key;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobId">任务标识Id</param>
        public static void RemoveJob(string jobId)
        {
            AsyncHelper.RunSync(() => _scheduler.DeleteJob(new JobKey(jobId)));
            _jobs.TryRemove(jobId, out _);
        }

        #endregion

        #region 内部类

        private class Job : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Task.Run(() =>
                {
                    string jobName = context.JobDetail.Key.Name;
                    if (_jobs.ContainsKey(jobName))
                    {
                        _jobs[jobName]?.Invoke();
                    }
                });
            }
        }

        #endregion
    }
}
