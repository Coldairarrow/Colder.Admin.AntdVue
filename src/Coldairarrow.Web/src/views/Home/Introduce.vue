<template>
  <a-card :bordered="false">
    <div>
      <p>本框架为.NET Core3.0+Ant Design Vue版</p>
      <p>本框架旨在极大的提高开发效率</p>
      <p>
        使用技术栈：<br />
        后端：采用.NET Core平台，ASP.NET Core3.0，C#语言（使用反射等高级技术），Entity
        FrameworkCore（数据库ORM框架）。<br />
        使用数据仓储模式，抽象化数据库操作(CRUD等)、支持事务处理以及分布式事务处理（跨库）<br />
        支持数据库读写分离、分库分表及事务(仅支持单表操作,不支持多表)
        全面采用Autofac作为IOC容器,面向接口编程,全面解耦<br />
        集成多种工具类库以及操作拓展<br />
        数据库：支持SqlServer,PostgreSQL,MySQL,Oracle（框架使用简单工厂，工厂方法，抽象工厂，可轻松更换数据库）,Redis作为分布式缓存<br />
        前端：Vue2.x全家桶+Ant Design Vue，其中集成常用组件，力求方便项目开发。
        <br />
        <br />
        具体技术实施：<br />
        项目采用前后端完全分离模式，并采用严格分层模式，极大的增加聚合度，降低耦合度，<br />
        提高代码的健壮性，可维护性。<br />
        前后端通过JWT进行身份验证,通过数据接口操作数据，统一使用JSON作为数据格式，并使用默认接口签名算法保证接口的安全性。
        <br />
        <br />
        功能架构部分详解：<br />
        快速开发：此功能为框架的核心，通过选择数据库中的表，就能自动生成对应的实体层、业务逻辑层、控制器、前端页面Vue文件，无需编写代码即可生成基本的CRUD操作。<br />
        接口密钥管理：管理接口的密钥，若开启接口签名的规则，则前端需要通过给接口签名才能够正常访问后台接口。<br />
        权限管理：使用基本的RBAC权限控制，支持操作权限、接口权限以及数据权限
        <br />
        <br />
        相关链接:<br />
        本框架链接,欢迎点星支持:<a href="https://github.com/Coldairarrow/Colder.Admin.AntdVue" target="_blank"
          >https://github.com/Coldairarrow/Colder.Admin.AntdVue</a
        ><br />
        个人GitHub,欢迎点星支持:<a href="https://github.com/Coldairarrow" target="_blank"
          >https://github.com/Coldairarrow</a
        ><br />
        个人博客:<a href="https://www.cnblogs.com/coldairarrow/" target="_blank"
          >https://www.cnblogs.com/coldairarrow/</a
        ><br />
        .NET Core3.0 下载:<a href="https://dotnet.microsoft.com/download" target="_blank"
          >https://dotnet.microsoft.com/download</a
        ><br />
        Vue官网:<a href="https://cn.vuejs.org/" target="_blank">https://cn.vuejs.org/</a><br />
        Ant Design Vue官网:<a href="https://www.antdv.com/docs/vue/introduce-cn/" target="_blank"
          >https://www.antdv.com/docs/vue/introduce-cn/</a
        ><br />
        Ant Design Vue pro官网:<a href="https://pro.loacg.com/" target="_blank">https://pro.loacg.com/</a><br /><br />
        个人QQ:862520575(欢迎商务合作及技术咨询)<br />
        QQ技术交流群2:579202910
      </p>
    </div>
  </a-card>
</template>