class SearchBook {
    constructor() {
        this.Init = (selector, donetHelper) => {
            this.InitAutoComplete(selector, donetHelper);
        };
    }
    InitAutoComplete(selector, donetHelper) {
        $(selector).kendoAutoComplete({
            dataTextField: "bookTitle",
            dataValueField: "id",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: "api/book/",
                }
            },
            select: (data) => {
                donetHelper.invokeMethodAsync('SetBookId', data.dataItem.id);
            }
        });
    }
}
window.searchBook = new SearchBook();
//# sourceMappingURL=App.js.map