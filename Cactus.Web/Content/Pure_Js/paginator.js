(function ($) {
    var state = {};
    $.fn.pure_paginator = function (options) {
        if (typeof options == "string") {
            return $.fn.pure_paginator.methods[options](this);
        }
        options = options || {};
        $(this).each(function () {
            state = $(this).data("paginator");
            if (state) {
                $.extend(state.options, options);
            }
            else {
                state = $(this).data("paginator", {
                    options: $.extend({}, $.fn.pure_paginator.defaults, options)
                });
                state = $(this).data("paginator");
            }
            bindPaginator($(this), state.options);
        })
    };
    var prev = 'pure-paginator-prev';
    var next = 'pure-paginator-next';
    var btn_class = 'pure-button';
    var btn_active = 'pure-button-active';
    var btn_theme = 'pure-button-primary';
    var temp = ' <li><a class="{1}" href="javascript:;">{0}</a></li>';
    var txt_temp = '<li><span>{0}</span></li>';
    var omit_html = txt_temp.replace('{0}', '....');
    function bindPaginator(jq, o_state) {

        var li_html = '';
        var li_html_left = '';
        var li_html_right = '';
        if (o_state.count <= 0) { return; }
        jq.html('');//清空
        var countStr = txt_temp.replace('{0}', o_state.countStr.replace('{count}', o_state.count));
        var prevStr = temp.replace('{0}', o_state.prevStr).replace('{1}', btn_class + ' ' + prev);
        var nextStr = temp.replace('{0}', o_state.nextStr).replace('{1}', btn_class + ' ' + next);
        if (o_state.count == 1) {
            li_html = prevStr + temp.replace('{0}', '1').replace('{1}', btn_class + ' ' + btn_theme + ' ' + btn_active) + nextStr;
            li_html += countStr;
        } else {
            var active = temp.replace('{0}', o_state.inPage).replace('{1}', btn_class + ' ' + btn_theme + ' ' + btn_active);
            for (var i = 1; i < parseInt(o_state.inPage) ; i++) {
                li_html_left += temp.replace('{0}', i).replace('{1}', btn_class);
            }
            for (var i = parseInt(o_state.inPage) + 1 ; i <= o_state.count; i++) {
                li_html_right += temp.replace('{0}', i).replace('{1}', btn_class);
            }
            li_html_left = left_html_parse(o_state.spacerNum, o_state.count, o_state.inPage);
            li_html_right = right_html_parse(o_state.spacerNum, o_state.count, o_state.inPage);
            li_html = prevStr + li_html_left + active + li_html_right + nextStr;
            li_html += countStr;
        }
        jq.html(li_html);
        bindEvent(jq, o_state);
    }
    function bindEvent(jq, o_state) {
        jq.find('.pure-button').bind('click', (function () {
            o_state.inPage = parseInt($(this).html());
            o_state.onPage(o_state.inPage);
        }));
        jq.find('.pure-button.' + prev).unbind('click');
        jq.find('.pure-button.' + prev).bind('click', (function () {
            console.log('prev:' + o_state.inPage);
            if (o_state.inPage == 1) {
                o_state.onPage(o_state.inPage);
            }
            else {
                o_state.onPage(o_state.inPage - 1);
            }
        }));
        jq.find('.pure-button.' + next).unbind('click');
        jq.find('.pure-button.' + next).bind('click', (function () {
            console.log('next:' + o_state.inPage);
            if (o_state.inPage == o_state.count) {
                o_state.onPage(o_state.inPage);
            }
            else {
                o_state.onPage(o_state.inPage + 1);
            }
        }))
    }
    //左边html
    function left_html_parse(spacerNum, count, inPage) {
        //if (inPage == 1) { ''; }
        var temp_html = '';
        var iii = inPage - 1 - 1;//左边的数量
        if (iii >= spacerNum) {
            temp_html += omit_html;
            for (var i = iii - spacerNum + 2; i < inPage; i++) {
                temp_html += temp.replace('{0}', i).replace('{1}', btn_class);
            }
        } else {
            for (var i = 1; i < inPage; i++) {
                temp_html += temp.replace('{0}', i).replace('{1}', btn_class);
            }
        }

        return temp_html;
    }
    //右边html
    function right_html_parse(spacerNum, count, inPage) {
        var temp_html = '';
        var iii = count - inPage - 1;//右边的数量
        if (iii >= spacerNum) {//大于间隔数
            for (var i = inPage + 1; i <= inPage + spacerNum; i++) {
                temp_html += temp.replace('{0}', i).replace('{1}', btn_class);
            }
            temp_html += omit_html;
        } else {
            for (var i = inPage + 1; i <= count; i++) {
                temp_html += temp.replace('{0}', i).replace('{1}', btn_class);
            }
        }
        return temp_html;
    }
    //方法
    $.fn.pure_paginator.methods = {
        inPage: function () { }
    };
    //默认项
    $.fn.pure_paginator.defaults = $.extend({}, {
        spacerNum: 2,//显示可以按下的按钮数量，不包含选中的,大于等于2
        prevStr: '上一页',
        nextStr: '下一页',
        countStr: '共{count}页',
        count: 10,//总页数
        inPage: 1,//当前页
        onPage: function (index) { }//按下的时候触发
    });
})(jQuery);
