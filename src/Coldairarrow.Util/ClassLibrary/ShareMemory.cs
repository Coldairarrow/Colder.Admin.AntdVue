using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 共享内存
    /// </summary>
    public class ShareMenmory
    {
        #region 导入类库及方法
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]

        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]

        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]

        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]

        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]

        public static extern bool CloseHandle(IntPtr handle);



        [DllImport("kernel32", EntryPoint = "GetLastError")]

        public static extern int GetLastError();



        const int ERROR_ALREADY_EXISTS = 183;



        const int FILE_MAP_COPY = 0x0001;

        const int FILE_MAP_WRITE = 0x0002;

        const int FILE_MAP_READ = 0x0004;

        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;



        const int PAGE_READONLY = 0x02;

        const int PAGE_READWRITE = 0x04;

        const int PAGE_WRITECOPY = 0x08;

        const int PAGE_EXECUTE = 0x10;

        const int PAGE_EXECUTE_READ = 0x20;

        const int PAGE_EXECUTE_READWRITE = 0x40;



        const int SEC_COMMIT = 0x8000000;

        const int SEC_IMAGE = 0x1000000;

        const int SEC_NOCACHE = 0x10000000;

        const int SEC_RESERVE = 0x4000000;



        const int INVALID_HANDLE_VALUE = -1;
        #endregion

        IntPtr m_hSharedMemoryFile = IntPtr.Zero;

        IntPtr m_pwData = IntPtr.Zero;//共享内存地址

        public bool m_bAlreadyExist = false;

        bool m_bInit = false;

        long m_MemSize = 0;

        public ShareMenmory(string strName, long lngSize)
        {
            if (OpenExists(strName, lngSize) == false)
                Init(strName, lngSize);
        }
        ~ShareMenmory()
        {
            Close();
        }

        //初始化内存
        public void Init(string strName, long lngSize)
        {
            m_MemSize = lngSize;
            if (strName.Length > 0)
            {
                //创建内存共享体(INVALID_HANDLE_VALUE)
                m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);
                if (m_hSharedMemoryFile == IntPtr.Zero)
                {
                    m_bAlreadyExist = false;
                    m_bInit = false;
                }
                else
                {
                    if (GetLastError() == ERROR_ALREADY_EXISTS)  //已经创建

                    {
                        m_bAlreadyExist = true;
                    }
                    else

                    {
                        m_bAlreadyExist = false;
                    }
                }
                //创建内存映射
                try
                {
                    m_pwData = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, 0);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (m_pwData == IntPtr.Zero)
                {
                    m_bInit = false;
                    CloseHandle(m_hSharedMemoryFile);
                }
                else
                {
                    m_bInit = true;
                    m_bAlreadyExist = true;
                }
            }
        }

        /// <summary>
        /// 获取共享内存
        /// </summary>
        /// <param name="mapName">内存名</param>
        /// <param name="Size">大小</param>
        public bool OpenExists(string mapName, long Size)
        {
            try
            {
                m_hSharedMemoryFile = OpenFileMapping(0x0002, true, mapName);
                m_pwData = MapViewOfFile(m_hSharedMemoryFile, 0x0002, 0, 0, (uint)Size);

                if (m_hSharedMemoryFile == IntPtr.Zero || m_pwData == IntPtr.Zero)
                    return false;
                m_MemSize = Size;
                m_bInit = true;
                m_bAlreadyExist = true;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //关闭共享内存

        public void Close()
        {

            if (m_bInit)

            {
                UnmapViewOfFile(m_pwData);

                CloseHandle(m_hSharedMemoryFile);
            }
        }
        /// <summary>
        /// 从共享内存读数据
        /// </summary>
        /// <param name="lngSize">数据长度</param>
        /// <param name="ofset">指针偏移量</param>
        /// <returns></returns>
        public byte[] Read(int lngSize, int ofset)
        {
            byte[] bytData = new byte[lngSize];
            Marshal.Copy(m_pwData + ofset, bytData, 0, lngSize);

            return bytData;
        }

        /// <summary>
        /// 将数据写入内存中
        /// </summary>
        /// <param name="bytData">需要写入的数据</param>
        /// <param name="offset">目的内存地址偏移量</param>
        public void Write(byte[] bytData, int offset)
        {
            Marshal.Copy(bytData, 0, m_pwData + offset, bytData.Length);
        }
    }

    /// <summary>
    /// 共享内存之操作泛型实体类
    /// </summary>
    /// <typeparam name="T">泛型参数</typeparam>
    public class ShareMenmory<T>
    {
        #region 常量
        const int ERROR_ALREADY_EXISTS = 183;

        const int FILE_MAP_COPY = 0x0001;

        const int FILE_MAP_WRITE = 0x0002;

        const int FILE_MAP_READ = 0x0004;

        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;



        const int PAGE_READONLY = 0x02;

        const int PAGE_READWRITE = 0x04;

        const int PAGE_WRITECOPY = 0x08;

        const int PAGE_EXECUTE = 0x10;

        const int PAGE_EXECUTE_READ = 0x20;

        const int PAGE_EXECUTE_READWRITE = 0x40;



        const int SEC_COMMIT = 0x8000000;

        const int SEC_IMAGE = 0x1000000;

        const int SEC_NOCACHE = 0x10000000;

        const int SEC_RESERVE = 0x4000000;



        const int INVALID_HANDLE_VALUE = -1;
        #endregion

        IntPtr m_hSharedMemoryFile = IntPtr.Zero;//共享内存空间指针

        IntPtr m_pwData = IntPtr.Zero;//共享内存地址

        public bool m_bAlreadyExist = false;

        bool m_bInit = false;

        long m_MemSize = 0;

        private int entityLength=0;
        public ShareMenmory(string strName, long lngSize)
        {
            if (OpenExists(strName, lngSize) == false)
                Init(strName, lngSize);

            Type t = typeof(T);
            object obj = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("getLength");
            entityLength = (int)method.Invoke(obj, null);
        }
        ~ShareMenmory()
        {
            Close();
        }

        //初始化内存
        public void Init(string strName, long lngSize)
        {
            m_MemSize = lngSize;
            if (strName.Length > 0)
            {
                //创建内存共享体(INVALID_HANDLE_VALUE)
                m_hSharedMemoryFile = ShareMenmory.CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);
                if (m_hSharedMemoryFile == IntPtr.Zero)
                {
                    m_bAlreadyExist = false;
                    m_bInit = false;
                }
                else
                {
                    if (ShareMenmory.GetLastError() == ERROR_ALREADY_EXISTS)  //已经创建

                    {
                        m_bAlreadyExist = true;
                    }
                    else

                    {
                        m_bAlreadyExist = false;
                    }
                }
                //创建内存映射
                try
                {
                    m_pwData = ShareMenmory.MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, 0);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (m_pwData == IntPtr.Zero)
                {
                    m_bInit = false;
                    ShareMenmory.CloseHandle(m_hSharedMemoryFile);
                }
                else
                {
                    m_bInit = true;
                    m_bAlreadyExist = true;
                }
            }
        }

        /// <summary>
        /// 获取共享内存
        /// </summary>
        /// <param name="mapName">内存名</param>
        /// <param name="Size">大小</param>
        public bool OpenExists(string mapName, long Size)
        {
            try
            {
                m_hSharedMemoryFile = ShareMenmory.OpenFileMapping(0x0002, true, mapName);
                m_pwData = ShareMenmory.MapViewOfFile(m_hSharedMemoryFile, 0x0002, 0, 0, (uint)Size);

                if (m_hSharedMemoryFile == IntPtr.Zero || m_pwData == IntPtr.Zero)
                    return false;
                m_MemSize = Size;
                m_bInit = true;
                m_bAlreadyExist = true;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //关闭共享内存

        public void Close()
        {

            if (m_bInit)

            {
                ShareMenmory.UnmapViewOfFile(m_pwData);

                ShareMenmory.CloseHandle(m_hSharedMemoryFile);
            }
        }
        /// <summary>
        /// 从共享内存读数据
        /// </summary>
        /// <param name="lngSize">数据长度</param>
        /// <param name="ofset">指针偏移量</param>
        /// <returns></returns>
        public byte[] Read(int lngSize, int ofset)
        {
            byte[] bytData = new byte[lngSize];
            Marshal.Copy(m_pwData + ofset, bytData, 0, lngSize);

            return bytData;
        }

        /// <summary>
        /// 将数据写入内存中
        /// </summary>
        /// <param name="bytData">需要写入的数据</param>
        /// <param name="offset">目的内存地址偏移量</param>
        public void Write(byte[] bytData, int offset)
        {
            Marshal.Copy(bytData, 0, m_pwData + offset, bytData.Length);
        }
        
        /// <summary>
        /// 获取URL
        /// </summary>
        /// <param name="index">所需要获取URL的索引序号</param>
        /// <returns></returns>
        public string GetUrl(int index)
        {
            int offset = 1 + index * (entityLength + 1)+8;
            return Extention.ToString(Read(200, offset)).Trim();
        }

        /// <summary>
        /// 保存URL
        /// </summary>
        /// <param name="index">保存的位置（索引序号）</param>
        /// <param name="url">URL</param>
        public void SetUrl(int index, string url)
        {
            int offset = 1 + index * (entityLength + 1) + 8;
            Write(url.ToBytes(), offset);
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="index">索引序号</param>
        /// <returns></returns>
        public string GetState(int index)
        {
            int offset = 1 + index * (entityLength + 1) + entityLength-2;
            return Extention.ToString(Read(1, offset));
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="index">索引序号</param>
        /// <param name="state">状态（0为未爬取，1为已经爬取）</param>
        public void SetState(int index,int state)
        {
            int offset = 1 + index * (entityLength + 1) + entityLength - 2;
            Write(state.ToString().ToBytes(),offset);
        }

        /// <summary>
        /// 保存实体类
        /// </summary>
        /// <param name="index">索引序号</param>
        /// <param name="t">实体类型</param>
        public void SetEntity(int index,T t)
        {
            int offset = index*entityLength;
            Write(t.EntityToJson().ToBytes(), offset);
        }

        public T GetEntity(int index)
        {
            string str = Extention.ToString(Read(entityLength, index * entityLength));
            return str.ToEntity<T>();
        }
    }
}
