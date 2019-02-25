declare var $;
class SearchBook {
    public Init = (selector, donetHelper) => {
        this.InitAutoComplete(selector, donetHelper);
    }
    public InitAutoComplete(selector: string, donetHelper) {
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

(<any>window).searchBook = new SearchBook();