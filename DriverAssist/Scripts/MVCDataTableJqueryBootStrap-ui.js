MVCDataTableJqueryBootStrap = {

    init: function () {
        this.initDataTable();
    },

    initDataTable: function () {
        var table = $('#tableContract').DataTable({

        });

        MVCDataTableJqueryBootStrap.returnDataTable = function () {
            return table;
        }
    },
};

$(function () {
    MVCDataTableJqueryBootStrap.init();
});