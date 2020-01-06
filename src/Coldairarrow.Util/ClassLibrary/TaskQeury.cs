using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 任务队列
    /// </summary>
    public class TaskQueue
    {
        #region 构造函数

        /// <summary>
        /// 默认队列
        /// 注：默认间隔时间1ms
        /// </summary>
        public TaskQueue()
        {
            _timeSpan = TimeSpan.Zero;
            Start();
        }

        /// <summary>
        /// 间隔任务队列
        /// 注：每个任务之间间隔一段时间
        /// </summary>
        /// <param name="timeSpan">间隔时间</param>
        public TaskQueue(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
            Start();
        }

        #endregion

        #region 私有成员
        Semaphore _semaphore { get; } = new Semaphore(0, int.MaxValue);
        private void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (_isRun)
                {
                    try
                    {
                        _semaphore.WaitOne();
                        bool success = _taskList.TryDequeue(out Action task);
                        if (success)
                        {
                            task?.Invoke();
                        }

                        if (_timeSpan != TimeSpan.Zero)
                            Thread.Sleep(_timeSpan);
                    }
                    catch (Exception ex)
                    {
                        HandleException?.Invoke(ex);
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
        private bool _isRun { get; set; } = true;
        private TimeSpan _timeSpan { get; set; }
        private ConcurrentQueue<Action> _taskList { get; } = new ConcurrentQueue<Action>();

        #endregion

        #region 外部接口

        public void Stop()
        {
            _isRun = false;
        }

        public void Enqueue(Action task)
        {
            _taskList.Enqueue(task);
            _semaphore.Release();
        }

        public Action<Exception> HandleException { get; set; }

        #endregion
    }
}
