(function ($) {
    $.extend(window, {
        "Html5Uploader": Html5Uploader
    });

    function Html5Uploader(container, options) {
        var defaults = {
            imgs: [],
            enableScale: false,
            asyncUpload: false,
            uploadUrl: null,
            maxHeight: 800,
            maxWidth: 800,
            uploadLimit: 5,  //上传数量限制，0为不限
            sizeLimit: 5,    //大小限制，M
            postData: null,
            type:'image' //默认图片,file则为文件
        };
        var imgItemHtml = [
            '<li class="up-pic">',
            '<div class="up-clip">',
            '<span data-src="" style="display:none"></span>',
            '<a href="" style="display:none"></a>',
            '<img src="" style="display:block;" />',
            '</div>',
            '<div class="up-mask"></div>',
            '<div class="up-progress">',
            '<div class="pos"></div>',
            '</div>',
            '<a class="up-del" href="javascript:void(0)" title="关闭">&nbsp;</a>',
            '</li>'
        ].join('');
        var $container, $imgWrap, $file;
        var files, fileCount = 0, uploadVerNo;

        function init() {
            options = $.extend({}, defaults, options);
            $container = $(container).addClass("up-container");
            if (!$container.length) {
                return;
            }

            $imgWrap = $container.find("ul.up-pics");
            if (!$imgWrap.length) {
                $imgWrap = $('<ul class="up-pics"></ul>').appendTo($container);
            }
            var accept = options.type == 'image' ? 'image/*' : '*/*';
            $file = $('<input type="file" class="up-file" accept="' + accept + '" />').appendTo($container);
            if (options.uploadLimit > 1) {
                $file.attr("multiple", "multiple");
            }
            $file.wrap('<div class="up-entry"></div>').change(function () {
                if (!this.files || !this.files.length) {
                    return;
                }

                uploadVerNo = Math.random();
                files = this.files;
                fileCount = files.length;
                asyncUpload(0);
            });

            $imgWrap.on("tap", "a.up-del", function () {
                $(this).closest("li.up-pic").remove();
                uploadCountCheck();
            });

            $imgWrap.on("click", "a.up-del", function () {
                $(this).closest("li.up-pic").remove();
                uploadCountCheck();
            });

            uploadCountCheck();

            var imgs = [];
            if (options.imgs) {
                var type = getType(options.imgs);
                switch (type) {
                    case 'string': imgs = options.imgs.split(','); break;
                    case 'array': imgs = options.imgs; break;
                    default: break;
                }
            }
            imgs.forEach(function (url) {
                insertItem(url);
            });
        }

        function asyncUpload(index) {
            if (index >= files.length) {
                $file.val("");
                return;
            }

            uploadFile(files[index], function () {
                asyncUpload(++index);
            });
        }

        //上传，可实现批量上传
        function uploadFile(file, onUploadComplete) {
            if (!file.type.match(/image.*/) && options.type == 'image') {
                alert("只能选择图片文件.");
                fileCount--;
                if (onUploadComplete) {
                    onUploadComplete.call();
                }
                return;
            }
            if (file.size > options.sizeLimit * 1024 * 1024) {
                alert("图片最大不能超过" + options.sizeLimit + "M.");
                fileCount--;
                if (onUploadComplete) {
                    onUploadComplete.call();
                }
                return;
            }
            if (isOverUploadLimit()) {
                alert("最多只能上传" + options.uploadLimit + "个文件.");
                fileCount = options.uploadLimit;
                if (onUploadComplete) {
                    onUploadComplete.call();
                }
                return;
            }

            //获取照片方向角属性，用户旋转控制  
            var orientation = null;
            if (options.enableScale) {
                EXIF.getData(file, function () {
                    EXIF.getAllTags(this);
                    orientation = EXIF.getTag(this, 'Orientation');
                });
            }

            var fileName = file.name;
            var fileType = file.type.indexOf("image/") == -1 ? 'file' : 'image';
            var $imgItem = $(imgItemHtml).appendTo($imgWrap);
            if (fileType == 'file') {
                $imgItem.find('img').css('display', 'none');
                $imgItem.find('div a').css('display', 'block');
            }

            function readFile() {
                var reader = new FileReader();
                reader.onloadend = function () {
                    if (reader.error) {
                        alert("文件获取失败！");
                        $imgItem.remove();
                        if (onUploadComplete) {
                            onUploadComplete.call();
                        }
                        return;
                    }
                    var fileData = reader.result; //"data:image/jpeg;base64,"开头

                    //第一段进度条
                    //$imgItem.find(".pos").animate({ width: "30%" }, 100, "linear");
                    if (fileType == 'image') {
                        $imgItem.find("img").attr({
                            "src": fileData,
                            "onload": loadimg
                        });
                    }

                    if (options.enableScale) {
                        scaleAndUpload(fileData);
                    } else {
                        upload(fileData);
                    }
                };
                reader.readAsDataURL(file);
            }

            //缩放后上传
            function scaleAndUpload(src) {
                var image = new Image();
                //绑定 load 事件处理器，加载完成后执行
                image.onload = function () {
                    var scaleWidth = this.naturalWidth,
                        scaleHeight = this.naturalHeight;

                    //// 如果高度超标
                    //if (scaleHeight > options.maxHeight) {
                    //    // 宽度等比例缩放 *=
                    //    scaleWidth *= options.maxHeight / scaleHeight;
                    //    scaleHeight = options.maxHeight;
                    //}
                    //if (scaleWidth > options.maxWidth) {
                    //    // 高度等比例缩放 *=
                    //    scaleHeight *= options.maxWidth / scaleWidth;
                    //    scaleWidth = options.maxWidth;
                    //}
                    //this.width = scaleWidth,
                    //this.height = scaleHeight;

                    var canvas = document.createElement("canvas");
                    var ctx = canvas.getContext("2d");
                    canvas.width = scaleWidth;
                    canvas.height = scaleHeight;
                    ctx.drawImage(this, 0, 0, scaleWidth, scaleHeight);

                    var fileData = null;
                    //修复ios  
                    if (navigator.userAgent.match(/iphone/i)) {
                        //如果方向角不为1，都需要进行旋转
                        if (orientation != "" && orientation != 1) {
                            switch (orientation) {
                                case 6://需要顺时针（向右）90度旋转  
                                    rotateImage(this, 'right', canvas);
                                    break;
                                case 8://需要逆时针（向左）90度旋转  
                                    rotateImage(this, 'left', canvas);
                                    break;
                                case 3://需要180度旋转
                                    rotateImage(this, 'left', canvas);//转两次  
                                    rotateImage(this, 'left', canvas);
                                    break;
                            }
                        }
                        fileData = canvas.toDataURL("image/jpeg", 0.9);
                    } else if (navigator.userAgent.match(/Android/i) && !true) {// 修复android  
                        var encoder = new JPEGEncoder();
                        fileData = encoder.encode(ctx.getImageData(0, 0, expectWidth, expectHeight), 80);
                    } else {
                        if (orientation != "" && orientation != 1) {
                            switch (orientation) {
                                case 6://需要顺时针（向右）90度旋转  
                                    rotateImage(this, 'right', canvas);
                                    break;
                                case 8://需要逆时针（向左）90度旋转  
                                    rotateImage(this, 'left', canvas);
                                    break;
                                case 3://需要180度旋转
                                    rotateImage(this, 'left', canvas);//转两次  
                                    rotateImage(this, 'left', canvas);
                                    break;
                            }
                        }
                        fileData = canvas.toDataURL("image/jpeg", 0.9);
                    }

                    upload(fileData);
                };

                //必须先绑定事件，才能设置src属性，否则会出同步问题。
                image.src = src;
            }

            //提交至服务器
            function upload(fileData) {

                //使用URL异步上传
                if (options.asyncUpload && options.uploadUrl) {
                    var postData = $.extend(options.postData, { fileName: fileName, fileType: fileType, data: fileData, count: fileCount, uploadVerNo: uploadVerNo });
                    $.post(options.uploadUrl, postData, function (res) {
                        if (!res.success) {
                            alert(res.message);
                            $imgItem.remove();
                            if (onUploadComplete) {
                                onUploadComplete.call();
                            }
                            return;
                        }

                        $imgItem.find('span').attr('data-src', res.src);
                        if (fileType == 'image') {
                            $imgItem.find("img").attr({ "src": res.src, "onload": loadimg });
                        } else {
                            $imgItem.find('div a').attr('href', res.src);
                            $imgItem.find('div a').text(fileName);
                        }

                        // 播放剩下的进度条动画
                        $imgItem.find(".pos").animate({ width: "100%" }, 0, "linear", function () {
                            $imgItem.addClass("up-over");

                            uploadCountCheck();

                            if (onUploadComplete) {
                                onUploadComplete.call();
                            }
                        });
                    }, "json");
                } else {
                    $imgItem.find("img").attr({ "src": fileData, "onload": loadimg });

                     //播放剩下的进度条动画
                    $imgItem.find(".pos").animate({ width: "100%" }, 500, "linear", function () {
                        $imgItem.addClass("up-over");

                        uploadCountCheck();

                        if (onUploadComplete) {
                            onUploadComplete.call();
                        }
                    });
                }
            }

            readFile();
        }

        //旋转图片
        function rotateImage(image, direction, canvas) {
            if (image == null) return;

            //最小与最大旋转方向，图片旋转4次后回到原方向    
            var min_step = 0,
                max_step = 3,
                step = 2,
                height = image.height,
                width = image.width;

            if (direction == 'left') {
                step++;
                step > max_step && (step = min_step);
            } else {
                step--;
                step < min_step && (step = max_step);
            }

            //旋转角度以弧度值为参数    
            var degree = step * 90 * Math.PI / 180;
            var ctx = canvas.getContext('2d');
            switch (step) {
                case 0:
                    canvas.width = width;
                    canvas.height = height;
                    ctx.drawImage(image, 0, 0, width, height);
                    break;
                case 1:
                    canvas.width = height;
                    canvas.height = width;
                    ctx.rotate(degree);
                    ctx.drawImage(image, 0, -height, width, height);
                    break;
                case 2:
                    canvas.width = width;
                    canvas.height = height;
                    ctx.rotate(degree);
                    ctx.drawImage(image, -width, -height, width, height);
                    break;
                case 3:
                    canvas.width = height;
                    canvas.height = width;
                    ctx.rotate(degree);
                    ctx.drawImage(image, -width, 0, width, height);
                    break;
            }
        }

        function loadimg() {
            var e = this.parentNode;
            if (e) {
                var t = this.offsetHeight - e.offsetHeight;
                var w = this.offsetWidth - e.offsetWidth;
                0 > t && (this.style.width = "auto", this.style.height = "100%");
                0 > w && (this.style.height = "auto", this.style.width = "100%");
            }
        }

        function uploadCountCheck() {
            if (isOverUploadLimit()) {
                $file.parent().hide();
            } else {
                $file.parent().show();
            }
        }

        function isOverUploadLimit() {
            return options.uploadLimit > 0 && $imgWrap.children().length >= options.uploadLimit;
        }

        //获得已上传图片路径
        function getUploadFiles() {
            var files = [];
            $imgWrap.find("span").each(function () {
                files.push($(this).attr("data-src"));
            });
            return files;
        }

        function insertItem(src) {
            var fileName = getFileName(src);
            var fileType = getFileType(fileName);
            var $imgItem = $(imgItemHtml).appendTo($imgWrap);
            $imgItem.addClass("up-over");
            if (fileType == 'image') {
                $imgItem.find("img").attr({ "src": src, "onload": loadimg });
            } else {
                $imgItem.find("img").css('display', 'none');
                $imgItem.find('div a').css('display', 'block');
                $imgItem.find('div a').attr('href', src);
                $imgItem.find('div a').text(fileName);
            }
            $imgItem.find('span').attr('data-src', src);

            uploadCountCheck();
        }

        function clear() {
            $imgWrap.empty();
        }

        return init(), {
            "getUploadFiles": getUploadFiles,
            "insertItem": insertItem,
            "clear": clear
        };

        function getFileType(fileName) {
            var suffixIndex = fileName.lastIndexOf(".");
            var suffix = fileName.substring(suffixIndex + 1).toUpperCase();
            if (suffix != "BMP" && suffix != "JPG" && suffix != "JPEG" && suffix != "PNG" && suffix != "GIF") {
                return 'file';
            } else {
                return 'image';
            }
        }

        function getFileName(src) {
            var str = src;
            var url = str.split('?')[0];
            var pathArray = url.split('/');

            return pathArray[pathArray.length - 1];
        }
    }
})(jQuery);