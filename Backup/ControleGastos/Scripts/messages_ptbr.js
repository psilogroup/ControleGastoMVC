jQuery.validator.addMethod("dateptbr", function (value, element) {
    return this.optional(element) || /\d{1,2}\/\d{1,2}\/\d{4}/.test(value);
}, "Data inválida");


jQuery.validator.addMethod("money", function (value, element) {
    return (this.optional(element) || /\d{1,6}\,\d{2,2}/.test(value) || /\d+/.test(value)) ;
}, "Valor inválido");
