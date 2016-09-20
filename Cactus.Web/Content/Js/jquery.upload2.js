/*******************************************************************************
* 异步上传文件,兼容IE8，火狐和谷歌可用,如果可以使用h5则使用h5
* 实现单个多次上传不刷新
* @author 柳伟伟 <702295399@qq.com>
* @version 1.5 (2016-05-09) 加入h5上传文件
*******************************************************************************/
(function ($) {
    var frameCount = 0;
    var formName = "";
    var iframeObj = null;
    var state = {};
    //var fileHtml = "";
    var colfile = null;
    //清空值
    function clean(target) {
        var file = $(target);
        var col = file.clone(true).val("");
        file.after(col);
        file.remove();
        //关键说明
        //先得到当前的对象和参数，接着进行克隆（同时克隆事件）
        //将克隆好的副本放在原先的之后，按照顺序逐个删除，最后初始化克隆的副本
    };
    function h5Submit(target) {
        var options = state.options;
        var fileObj = target[0].files[0];

        var fd = new FormData();//h5对象
        //附加参数
        for (key in options.params) {
            fd.append(key, options.params[key])
        }
        var fileName = target.attr('name');
        if (fileName == ''
            || fileName==undefined) {
            fileName = 'file';
        }
        fd.append(fileName, fileObj);
        //异步上传
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", function (evt) {
            if (evt.lengthComputable) {
                var percentComplete = Math.round(evt.loaded * 100 / evt.total);
                console.log(percentComplete + "%");
                if (options.onProgress) {
                    options.onProgress(evt);
                }
            }
        }, false);
        xhr.addEventListener("load", function (evt) {
            if ('json' == options.dataType) {
                var d = window.eval('(' + evt.target.responseText + ')');
                options.onComplate(d);
            } else {
                options.onComplate(evt.target.responseText);
            }
        }, false);
        xhr.addEventListener("error", function () {
            console.log("error");
        }, false);
        xhr.open("POST", options.url);
        xhr.send(fd);
    }
    function ajaxSubmit(target) {
        var options = state.options;
        if (options.url == '' || options.url == null) {
            alert("无上传地址");
            return;
        }
        if ($(target).val() == '' || $(target).val() == null) {
            alert("请选择文件");
            return;
        }
        var canSend = options.onSend($(target), $(target).val());
        if (!canSend) {
            return;
        }
        /*判断是否可以用h5*/
        if (window.FormData) {
            //h5
            console.log('h5Submit');
            h5Submit(target);
        } else {
            /**/
            if (iframeObj == null) {
                var frameName = 'upload_frame_' + (frameCount++);
                var iframe = $('<iframe style="position:absolute;top:-9999px" ><script type="text/javascript"></script></iframe>').attr('name', frameName);
                formName = 'form_' + frameName;
                var form = $('<form method="post" style="display:none;" enctype="multipart/form-data" />').attr('name', formName);
                form.attr("target", frameName).attr('action', options.url);
                //
                var fileHtml = $(target).prop("outerHTML");
                colfile = $(target).clone(true);
                $(target).replaceWith(colfile);
                var formHtml = "";
                // form中增加数据域
                for (key in options.params) {
                    formHtml += '<input type="hidden" name="' + key + '" value="' + options.params[key] + '">';
                }
                form.append(formHtml);
                form.append(target);
                iframe.appendTo("body");
                form.appendTo("body");
                iframeObj = iframe;
            }
            //禁用
            $(colfile).attr("disabled", "disabled");
            var form = $("form[name=" + formName + "]");
            //加载事件
            iframeObj.bind("load", function (e) {
                var contents = $(this).contents().get(0);
                var data = $(contents).find('body').text();
                if ('json' == options.dataType) {
                    try {
                        data = window.eval('(' + data + ')');
                    }
                    catch (Eobject) {
                        console.log('返回的json数据错误');
                        console.log(Eobject);
                        data = null;
                    }
                }
                options.onComplate(data);
                iframeObj.remove();
                form.remove();
                iframeObj = null;
                //启用
                $(colfile).removeAttr("disabled");
            });
            try {
                form.submit();
            } catch (Eobject) {
                console.log(Eobject);
            }
        }
    };
    //构造
    $.fn.upload = function (options) {
        if (typeof options == "string") {
            return $.fn.upload.methods[options](this);
        }
        options = options || {};
        state = $.data(this, "upload");
        if (state)
            $.extend(state.options, options);
        else {
            state = $.data(this, "upload", {
                options: $.extend({}, $.fn.upload.defaults, options)
            });
        }
    };
    //方法
    $.fn.upload.methods = {
        clean: function (jq) {
            return jq.each(function () {
                clean(jq);
            });
        },
        ajaxSubmit: function (jq) {
            return jq.each(function () {
                ajaxSubmit(jq);
            });
        },
        getFileVal: function (jq) {
            return jq.val()
        }
    };
    //默认项
    $.fn.upload.defaults = $.extend({}, {
        url: '',
        dataType: 'json',
        params: {},
        onSend: function (obj, str) { return true; },
        onComplate: function (e) { },
        onProgress: function (e) { }
    });
})(jQuery);
