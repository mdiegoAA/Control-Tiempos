// moment
moment.locale("es");

// bootboxjs
bootbox.setLocale("es");

$('.select2').select2({
    language: "es",
    dropdownAutoWidth: true,
    width: '100%'
});

// jQuery Validation
$.validator.setDefaults({
    highlight: function(element) {
        $(element).closest(".control-group").addClass("has-error");
    },
    unhighlight: function(element) {
        $(element).closest(".control-group").removeClass("has-error");
    },
    errorElement: "span",
    errorClass: "help-block",
    errorPlacement: function(error, element) {
        if (element.parent(".controls").length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});

$.validator.addMethod("date", function(value, element) {
    return this.optional(element) || moment(value, "DD/MM/YYYY");
});

$.validator.addMethod("isdateafter", function (value, element, params) {
    value = moment(value, "DD/MM/YYYY");
    var otherDate = moment($(params.compareTo).val(), "DD/MM/YYYY");
    console.log(value, otherDate);
    if (isNaN(value) || isNaN(otherDate))
        return true;

    return value > otherDate || (value === otherDate && params.allowEqualDates === "True");
});

$.validator.unobtrusive.adapters.add("isdateafter", ["propertytested", "allowequaldates"], function(options) {
    options.rules["isdateafter"] = {
        'allowEqualDates': options.params["allowequaldates"],
        'compareTo': "#" + options.params["propertytested"]
    };
    options.messages["isdateafter"] = options.message;
});

(function($) {
    $.fn.resetValidation = function() {
        var $form = $(this);

        $form.validate().resetForm();

        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul")
            .empty();

        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        $form.find(".control-group")
            .removeClass("has-error");

        return $form;
    };
})(jQuery);

// KO Bindings

ko.bindingHandlers.datetimepicker = {
    init: function(element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datetimepickerOptions || {}, $el = $(element);

        $el.datetimepicker(options);

        //handle the field changing by registering datepicker's dp.change event
        ko.utils.registerEventHandler(element, "dp.change", function() {
            var observable = valueAccessor();
            observable($el.data("DateTimePicker").date());
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $el.datepicker("destroy");
        });
    },
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()), $el = $(element);

        //handle date data coming via json from Microsoft
        if (String(value).indexOf("/Date(") === 0) {
            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }

        var current = $el.data("DateTimePicker").date();

        if (value !== current) {
            $el.data("DateTimePicker").date(value);
        }
    }
};

ko.bindingHandlers.select2 = {
    init: function(el, valueAccessor, allBindingsAccessor, viewModel) {
        ko.utils.domNodeDisposal.addDisposeCallback(el, function() {
            $(el).select2('destroy');
        });

        var allBindings = allBindingsAccessor(),
            select2 = ko.utils.unwrapObservable(allBindings.select2);

        $(el).select2(select2);
    },
    update: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();

        if ("value" in allBindings) {
            if ((allBindings.select2.multiple || el.multiple) && allBindings.value().constructor != Array) {                
                $(el).val(allBindings.value().split(',')).trigger('change');
            }
            else {
                $(el).val(allBindings.value()).trigger('change');
            }
        } else if ("selectedOptions" in allBindings) {
            var converted = [];
            var textAccessor = function(value) { return value; };
            if ("optionsText" in allBindings) {
                textAccessor = function(value) {
                    var valueAccessor = function (item) { return item; }
                    if ("optionsValue" in allBindings) {
                        valueAccessor = function (item) { return item[allBindings.optionsValue]; }
                    }
                    var items = $.grep(allBindings.options(), function (e) { return valueAccessor(e) == value});
                    if (items.length == 0 || items.length > 1) {
                        return "UNKNOWN";
                    }
                    return items[0][allBindings.optionsText];
                }
            }
            $.each(allBindings.selectedOptions(), function (key, value) {
                converted.push({id: value, text: textAccessor(value)});
            });
            $(el).select2("data", converted);
        }
    }
};