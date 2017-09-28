String.prototype.getBytesLength = function () {
    return this.replace(/[^\x00-\xff]/gi, "--").length;
}

jQuery.fn.extend({
    formSerialize : function () {
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