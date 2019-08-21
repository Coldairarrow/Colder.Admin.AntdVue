using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coldairarrow.Business.Base_SysManage
{
    public class RapidDevelopmentBusiness : BaseBusiness<Base_DatabaseLink>, IRapidDevelopmentBusiness, IDependency
    {
        #region DI

        public RapidDevelopmentBusiness(IHostingEnvironment hostingEnvironment)
        {
            _contentRootPath = $"{hostingEnvironment.ContentRootPath}\\";
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取所有数据库连接
        /// </summary>
        /// <returns></returns>
        public List<Base_DatabaseLink> GetAllDbLink()
        {
            return GetList();
        }

        /// <summary>
        /// 获取数据库所有表
        /// </summary>
        /// <param name="linkId">数据库连接Id</param>
        /// <returns></returns>
        public List<DbTableInfo> GetDbTableList(string linkId)
        {
            if (linkId.IsNullOrEmpty())
                return new List<DbTableInfo>();
            else
                return GetTheDbHelper(linkId).GetDbAllTables();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="linkId">连接Id</param>
        /// <param name="areaName">区域名</param>
        /// <param name="tables">表列表</param>
        /// <param name="buildType">需要生成类型</param>
        public void BuildCode(string linkId, string areaName, string tables, string buildType)
        {
            //内部成员初始化
            _dbHelper = GetTheDbHelper(linkId);
            GetDbTableList(linkId).ForEach(aTable =>
            {
                _dbTableInfoDic.Add(aTable.TableName, aTable);
            });

            List<string> tableList = tables.ToList<string>();
            List<string> buildTypeList = buildType.ToList<string>();
            tableList.ForEach(aTable =>
            {
                var tableFieldInfo = _dbHelper.GetDbTableInfo(aTable);
                //实体层
                if (buildTypeList.Exists(x => x.ToLower() == "entity"))
                {
                    BuildEntity(tableFieldInfo, areaName, aTable);
                }
                //业务层
                if (buildTypeList.Exists(x => x.ToLower() == "business"))
                {
                    BuildIBusiness(areaName, aTable);
                    BuildBusiness(areaName, aTable);
                }
                //控制器
                if (buildTypeList.Exists(x => x.ToLower() == "controller"))
                {
                    BuildController(areaName, aTable);
                }
                //视图
                if (buildTypeList.Exists(x => x.ToLower() == "view"))
                {
                    BuildView(tableFieldInfo, areaName, aTable);
                }
            });
        }

        #endregion

        #region 私有成员
        private string _contentRootPath { get; }
        private void BuildEntity(List<TableInfo> tableInfo, string areaName, string tableName)
        {
            string rootPath = _contentRootPath;
            string entityPath = rootPath.Replace("Coldairarrow.Web", "Coldairarrow.Entity") + areaName;
            string filePath = $@"{entityPath}\{tableName}.cs";
            string nameSpace = $@"Coldairarrow.Entity.{areaName}";

            _dbHelper.SaveEntityToFile(tableInfo, tableName, _dbTableInfoDic[tableName].Description, filePath, nameSpace);
        }
        private void BuildIBusiness(string areaName, string entityName)
        {
            string className = $"I{entityName}Business";
            string code =
$@"using Coldairarrow.Entity.{areaName};
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.{areaName}
{{
    public interface {className}
    {{
        List<{entityName}> GetDataList(Pagination pagination, string condition, string keyword);
        {entityName} GetTheData(string id);
        AjaxResult AddData({entityName} newData);
        AjaxResult UpdateData({entityName} theData);
        AjaxResult DeleteData(List<string> ids);
    }}
}}";
            string rootPath = _contentRootPath.Replace("Coldairarrow.Web", "Coldairarrow.Business"); ;
            string filePath = Path.Combine(rootPath, "IBusiness", areaName, $"{className}.cs");

            FileHelper.WriteTxt(code, filePath, FileMode.Create);
        }
        private void BuildBusiness(string areaName, string entityName)
        {
            string className = $"{entityName}Business";
            string code =
$@"using Coldairarrow.Entity.{areaName};
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.{areaName}
{{
    public class {className} : BaseBusiness<{entityName}>, I{className}, IDependency
    {{
        #region 外部接口

        public List<{entityName}> GetDataList(Pagination pagination, string condition, string keyword)
        {{
            var q = GetIQueryable();
            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@""{{condition}}.Contains(@0)"", keyword);

            return q.GetPagination(pagination).ToList();
        }}

        public {entityName} GetTheData(string id)
        {{
            return GetEntity(id);
        }}

        public AjaxResult AddData({entityName} newData)
        {{
            Insert(newData);

            return Success();
        }}

        public AjaxResult UpdateData({entityName} theData)
        {{
            Update(theData);

            return Success();
        }}

        public AjaxResult DeleteData(List<string> ids)
        {{
            Delete(ids);

            return Success();
        }}

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }}
}}";
            string rootPath = _contentRootPath.Replace("Coldairarrow.Web", "Coldairarrow.Business"); ;
            string filePath = Path.Combine(rootPath, "Business", areaName, $"{className}.cs");

            FileHelper.WriteTxt(code, filePath, FileMode.Create);
        }
        private void BuildController(string areaName, string entityName)
        {
            string ibusName = $"I{entityName}Business";
            string varBusiness = $@"{entityName.ToFirstLowerStr()}Bus";
            string _varBusiness = $@"_{entityName.ToFirstLowerStr()}Bus";
            string code =
$@"using Coldairarrow.Business.{areaName};
using Coldairarrow.Entity.{areaName};
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Areas.{areaName}.Controllers
{{
    [Area(""{areaName}"")]
    public class {entityName}Controller : BaseMvcController
    {{
        #region DI

        public {entityName}Controller({ibusName} {varBusiness})
        {{
            {_varBusiness} = {varBusiness};
        }}
        {ibusName} {_varBusiness} {{ get; }}

        #endregion

        #region 视图功能

        public ActionResult Index()
        {{
            return View();
        }}

        public ActionResult Form(string id)
        {{
            var theData = id.IsNullOrEmpty() ? new {entityName}() : {_varBusiness}.GetTheData(id);

            return View(theData);
        }}

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name=""pagination"">分页参数</param>
        /// <param name=""condition"">查询类型</param>
        /// <param name=""keyword"">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(Pagination pagination, string condition, string keyword)
        {{
            var dataList = {_varBusiness}.GetDataList(pagination, condition, keyword);

            return DataTable_Bootstrap(dataList, pagination);
        }}

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name=""theData"">保存的数据</param>
        public ActionResult SaveData({entityName} theData)
        {{
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {{
                theData.Id = IdHelper.GetId();

                res = {_varBusiness}.AddData(theData);
            }}
            else
            {{
                res = {_varBusiness}.UpdateData(theData);
            }}

            return JsonContent(res.ToJson());
        }}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name=""theData"">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {{
            var res = {_varBusiness}.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }}

        #endregion
    }}
}}";
            string rootPath = _contentRootPath;
            string filePath = $@"{rootPath}Areas\{areaName}\Controllers\{entityName}Controller.cs";

            FileHelper.WriteTxt(code, filePath, FileMode.Create);
        }
        private void BuildView(List<TableInfo> tableInfoList, string areaName, string entityName)
        {
            //生成Index页面
            StringBuilder searchConditionSelectHtml = new StringBuilder();
            StringBuilder tableColsBuilder = new StringBuilder();
            StringBuilder formRowBuilder = new StringBuilder();
            var formHeight = tableInfoList.Where(x => x.Name != "Id").Count() * 2;
            if (formHeight > 8)
                formHeight = 8;
            tableInfoList.Where(x => x.Name != "Id").ForEach((aField, index) =>
                {
                    //搜索的下拉选项
                    Type fieldType = _dbHelper.DbTypeStr_To_CsharpType(aField.Type);
                    if (fieldType == typeof(string))
                    {
                        string newOption = $@"
                    <option value=""{aField.Name}"">{aField.Description}</option>";
                        searchConditionSelectHtml.Append(newOption);
                    }

                    //数据表格列
                    string newCol = $@"
                {{ title: '{aField.Description}', field: '{aField.Name}', width: '5%' }},";
                    tableColsBuilder.Append(newCol);

                    //Form页面中的Html
                    string newFormRow = $@"
        <div class=""form-group form-group-sm"">
            <label class=""col-sm-2 control-label"">{aField.Description}</label>
            <div class=""col-sm-5"">
                <input name=""{aField.Name}"" value=""@obj.{aField.Name}"" type=""text"" class=""form-control"" required>
                <div class=""help-block with-errors""></div>
            </div>
        </div>";
                    formRowBuilder.Append(newFormRow);
                });
            string indexHtml =
$@"@{{
    Layout = ""~/Views/Shared/_Layout_List.cshtml"";
}}

<div class=""fx-content"">
    <div id=""toobar"">
        <div class=""btn-group btn-group-sm"">
            <button onclick=""openForm('', '添加数据');"" type=""button"" class=""btn btn-default"" aria-label=""Left Align"">
                <span class=""glyphicon glyphicon-plus"" aria-hidden=""true""></span>
                添加
            </button>
            <button onclick=""deleteData();"" type=""button"" class=""btn btn-default"" aria-label=""Right Align"">
                <span class=""glyphicon glyphicon-trash"" aria-hidden=""true""></span>
                删除
            </button>
            <button type=""button"" class=""btn btn-default"" aria-label=""Right Align"" onclick=""javascript: location.reload();"">
                <span class=""glyphicon glyphicon-refresh"" aria-hidden=""true""></span>
                刷新
            </button>
        </div>
    </div>
    <div class=""fx-wrapper"">
        <form class=""form-inline"" id=""searchForm"">
            <div class=""form-group"">
                <label>查询类别</label>
                <select class=""selectpicker"" name=""condition"" data-style=""btn-default btn-sm"" data-width=""100px"">
                    <option value="""">请选择</option>
                    {searchConditionSelectHtml.ToString()}
                </select>
                <input type=""text"" class=""form-control input-sm"" name=""keyword"" placeholder=""请输入关键字"">
            </div>
            <div class=""form-group"">
                <button type=""button"" class=""btn btn-default btn-sm"" onclick=""javascript: $('#dataTable').bootstrapTable('refresh', {{ silent: true }});"">
                    <i class=""glyphicon glyphicon-search""></i>
                    查询
                </button>
            </div>
        </form>
    </div>
    <div class=""fx-wrapper"">
        <table id=""dataTable"" class=""table-bordered""></table>
    </div>
</div>

<script>
    var $table = $('#dataTable');

    $(function () {{
        initTable();
        bindEvent();
    }});

    //初始化表格
    function initTable() {{
        $table.bootstrapTable({{
            url: '/{areaName}/{entityName}/GetDataList',
            idField: 'Id',
            method: 'post',
            contentType: 'application/x-www-form-urlencoded',
            queryParamsType: '',
            clickToSelect: false,
            pagination: true,
            sidePagination: ""server"",
            pageNumber: 1,
            pageSize: 30,
            pageList: [10, 30, 60, 100],
            columns: [
                {{ title: 'ck', field: 'ck', checkbox: true, width: '3%' }},{tableColsBuilder.ToString()}
                {{
                    title: '操作', field: '_', width: '80%', formatter: function (value, row) {{
                        var builder = new BtnBuilder();
                        builder.AddBtn({{ icon: 'glyphicon-edit', function: 'openForm', param: [row['Id']] }});
                        builder.AddBtn({{ icon: 'glyphicon-trash', function: 'deleteData', param: [row['Id']] }});

                        return builder.build();
                    }}
                }}
            ],
            queryParams: function (params) {{
                var searchParams = $('#searchForm').getValues();
                $.extend(params, searchParams);

                return params;
            }}
        }});
    }}

    //绑定事件
    function bindEvent() {{

    }}

    //打开表单
    function openForm(id, title) {{
        dialogOpen({{
            id: 'form',
            title: title,
            btn: ['确定', '取消'],
            height:'{formHeight}0%',
            url: '/{areaName}/{entityName}/Form?id={{0}}'.format(id || ''),
            yes: function (window, body) {{
                window.submitForm();
            }}
        }});
    }}

    //删除数据
    function deleteData(id) {{
        dialogComfirm('确认删除吗？', function () {{
            var ids = [];

            if (typeof (id) == 'string') {{//单条数据
                ids.push(id);
            }} else {{//多条数据
                var rows = $table.bootstrapTable('getSelections');
                if (rows.length == 0) {{
                    dialogError('请选择需要删除的数据！');
                    return;
                }} else {{
                    $.each(rows, function (index, value) {{
                        ids.push(value['Id']);
                    }})
                }}
            }}

            loading();
            $.postJSON('/{areaName}/{entityName}/DeleteData', {{ ids: JSON.stringify(ids) }}, function (resJson) {{
                loading(false);

                if (resJson.Success) {{
                    $table.bootstrapTable('refresh');
                    dialogSuccess('删除成功!');
                }}
                else {{
                    dialogError(resJson.Msg);
                }}
            }});
        }});
    }}
</script>
";
            string rootPath = _contentRootPath;
            string indexPath = $@"{rootPath}Areas\{areaName}\Views\{entityName}\Index.cshtml";

            FileHelper.WriteTxt(indexHtml, indexPath, FileMode.Create);

            //生成Form页面
            string formHtml =
$@"@using Coldairarrow.Entity.{areaName};
@using Coldairarrow.Util;

@{{
    Layout = ""~/Views/Shared/_Layout_Form.cshtml"";

    var obj = ({entityName})Model;
    var objStr = Html.Raw(obj.ToJson());
}}
<div style=""padding:15px;padding-right:45px;"">
    <form id=""form"" class=""form-horizontal"" role=""form"">
        {formRowBuilder.ToString()}
        <div class=""form-group"">
            <button id=""submit"" type=""submit"" class=""hidden"">提交</button>
        </div>
    </form>
</div>
<script>
    var theEntity = @objStr;

    $(function () {{
        initEvent();
    }});

    //事件绑定
    function initEvent() {{
        //表单校验
        $('#form').validator().on('submit', function (e) {{
            //校验成功
            if (!e.isDefaultPrevented()) {{
                e.preventDefault();

                var values = $('#form').getValues();
                
                $.extend(theEntity, values);
                loading();
                $.postJSON('/{areaName}/{entityName}/SaveData', theEntity, function (resJson) {{
                    loading(false);

                    if (resJson.Success) {{
                        parent.$('#dataTable').bootstrapTable('refresh');
                        parent.dialogSuccess();
                        dialogClose();
                    }}
                    else {{
                        dialogError(resJson.Msg);
                    }}
                }});
            }}
        }})
    }}

    //提交表单
    function submitForm() {{
        $('#submit').trigger('click');
    }}
</script>";
            string formPath = $@"{rootPath}Areas\{areaName}\Views\{entityName}\Form.cshtml";

            FileHelper.WriteTxt(formHtml, formPath, FileMode.Create);
        }
        private DbHelper GetTheDbHelper(string linkId)
        {
            var theLink = GetTheLink(linkId);
            DbHelper dbHelper = DbHelperFactory.GetDbHelper(theLink.DbType, theLink.ConnectionStr);

            return dbHelper;
        }
        private Base_DatabaseLink GetTheLink(string linkId)
        {
            Base_DatabaseLink resObj = new Base_DatabaseLink();
            var theModule = GetIQueryable().Where(x => x.Id == linkId).FirstOrDefault();
            resObj = theModule ?? resObj;

            return resObj;
        }
        private DbHelper _dbHelper { get; set; }
        private Dictionary<string, DbTableInfo> _dbTableInfoDic { get; set; } = new Dictionary<string, DbTableInfo>();

        #endregion

        #region 数据模型

        #endregion
    }
}
