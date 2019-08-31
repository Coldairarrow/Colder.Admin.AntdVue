(function ($) {

	if (!$) return;

	$.fn.showZTreeDrop = function () {

		var hideZtreeDropMenu = function () { $('.ztree-dropmenu.open').fadeOut('fast').removeClass('open'); }

		$('body').bind('mousedown', function (event) {
			var $target = $(event.target);
			//if (!($target.hasClass('input-group-addon') || $target.parents('.input-group-addon').length > 0 || $target.hasClass('ztree-dropmenu') || $(event.target).parents('.ztree-dropmenu').length > 0)) hideZtreeDropMenu();
			if (!($target.parents('[data-role="ztree"]').length > 0)) hideZtreeDropMenu();
		});

		//$('body').bind('mousedown', function (event) {
		//	var $target = $(event.target);
		//	//if (!($target.hasClass('input-group-addon') || $target.parents('.input-group-addon').length > 0 || $target.hasClass('ztree-dropmenu') || $(event.target).parents('.ztree-dropmenu').length > 0)) hideZtreeDropMenu();
		//	if (!($target.parents('[data-role="ztree"]').length > 0)) hideZtreeDropMenu();
		//});

		$(this).each(function (index, ele) {

			var $this = $(ele), asyncurl = $this.attr('data-url'), field = $this.attr('data-field'), check = $this.attr('data-check');
			var $form = $this.parents('form'), $valueElement = $form.find('#' + field), $displayElement = $this.find('input[type="text"]'), $addon = $this.find('.input-group-addon').css('cursor', 'pointer'), $dropmenu = $this.find('.ztree-dropmenu'), $ztree = $this.find('.ztree');

			$addon.bind('click', function () {
				var _width = $this.width();
				$dropmenu.css({ width: _width }).slideDown('fast').addClass('open');
			});
			$displayElement.bind('click', function () { $addon.click() });

			var _setting = {
				check: { enable: check ? true : false },
				async: { enable: true, url: asyncurl, autoParam: ["id=pid"], type: 'get' },
				view: {
					dblClickExpand: false,
					selectedMulti: false
				},
				data: { simpleData: { enable: true } },
				edit: { enable: false },
				callback: {
					beforeClick: function (treeId, treeNode, clickFlag) {
						//var zTree = $.fn.zTree.getZTreeObj($ztree.attr('id'));
						//zTree.expandNode(treeNode);
						//判断是否为末级项
						//var check = (treeNode && !treeNode.isParent);
						//check ? zTree.expandNode(treeNode) : check;
						//return check;
					},
					onClick: function (e, treeId, treeNode) {
						var zTree = $.fn.zTree.getZTreeObj($ztree.attr('id')), nodes = zTree.getSelectedNodes(), id = nodes[0].id, name = nodes[0].name;
						$displayElement.val(name); $valueElement.val(id);
						hideZtreeDropMenu();
					}
				}
			};

			//初始化
			$.fn.zTree.init($ztree, _setting);

		})
	}

})(jQuery);


//应用例子:
//$('[data-role="ztree"]').showZTreeDrop();