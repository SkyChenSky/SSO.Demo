jQuery.extend({
    openLayer: function (url, params, title) {
        layui.use('layer',
            function () {
                var $ = layui.$;
                var layer = layui.layer;

                var loadHtml = $.ajax({
                    url: url,
                    type: "Get",
                    async: false,
                    data: params
                }).responseText;

                layer.open({
                    type: 1,
                    skin: 'layui-layer-rim', //加上边框
                    content: loadHtml,
                    area: ['600px'],
                    title: title,
                    success: function () {
                        var form = $("form")
                            .removeData("validator")
                            .removeData("unobtrusiveValidation");

                        $.validator.unobtrusive.parse(form);
                    }
                });
            });
    },
    confirmDelete: function (url, params, reload) {
        layer.confirm('是否确认删除？',
            function (index) {
                $.post(url,
                    params,
                    function (result) {
                        if (result.success) {
                            reload();
                            layer.close(index);
                            layer.msg(result.message);
                        } else
                            layer.msg(result.message);
                    });
            });
    },
    confirmEdit: function (formSelector, params, reload) {
        var $form = $(formSelector);
        var isvaild = $form.valid();
        if (!isvaild)
            return false;

        layer.confirm('是否确认提交？',
            function () {
                var action = $form.prop("action");

                $.post(action,
                    params,
                    function (result) {
                        if (result.success) {
                            layer.closeAll();
                            layer.msg(result.message);

                            reload();
                        } else
                            layer.msg(result.message);
                    });
            });
        return false;
    }
});

String.prototype.getBytesLength = function () {
    return this.replace(/[^\x00-\xff]/gi, "--").length;
}

jQuery.fn.extend({
    formSerialize: function () {
        var data = {};
        $('input,select,textarea', this).each(function () {
            var element = $(this);
            if (!element.prop("disabled") && element.attr('name')) {
                var key = element.attr('name');
                data[key] = element.val();
            }
        });
        return data;
    }
});

jQuery.fn.extend({
    'bindSelectData': function (url, value) {
        var $self = $(this);
        if (!$self.is('select')) {
            throw new Error('bindSelectData\'s extension requires select tag');
        }

        $.getJSON(url, { value: value }).done(function (d) {
            var html = "";

            if (d && d.length > 0) {
                $.each(d, function (i, v) {
                    html += '<option ' + (v.selected === true ? 'selected="selected"' : '') + ' value="' + v.value + '">' + v.text + '</option>';
                });
            }

            $self.append(html);

            layui.use('form', function () {
                var form = layui.form;
                form.render('select');
            });
        });
    }
});

$.validator.addMethod("charlength", function (value, element, params) {
    if (this.optional(element)) {
        return true;
    }
    var maxlength = parseInt(params.maxlength);

    return value.getBytesLength() <= maxlength;
});

$.validator.unobtrusive.adapters.add("charlength", ["maxlength"], function (options) {
    options.rules['charlength'] = {
        maxlength: options.params.maxlength
    };
    options.messages['charlength'] = options.message;
});

$.validator.addMethod("numberminvalue", function (value, element, params) {
    if (this.optional(element)) {
        return true;
    }
    var minvalue = parseFloat(params.minvalue);

    return value >= minvalue;
});

$.validator.unobtrusive.adapters.add("numberminvalue", ["minvalue"], function (options) {
    options.rules['numberminvalue'] = {
        minvalue: options.params.minvalue
    };
    options.messages['numberminvalue'] = options.message;
});