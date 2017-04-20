// Write your Javascript code.

//var client = AzureSearch({
//    url: "https://search-bdkxnzfm7d32e.search.windows.net",
//    key: "6426C940A6181635163A5A187D918E8E", 
//});

//var searchIndexUrl = "https://search-bdkxnzfm7d32e.search.windows.net";
//var apiKey = "6426C940A6181635163A5A187D918E8E";


////var initialData = [
////    { name: "Well-Travelled Kitten", sales: 352, price: 75.95 },
////    { name: "Speedy Coyote", sales: 89, price: 190.00 },
////    { name: "Furious Lizard", sales: 152, price: 25.00 },
////    { name: "Indifferent Monkey", sales: 1, price: 99.95 },
////    { name: "Brooding Dragon", sales: 0, price: 6350 },
////    { name: "Ingenious Tadpole", sales: 39450, price: 0.35 },
////    { name: "Optimistic Snail", sales: 420, price: 1.50 }
////];

////{
////    beerName: "Abita Turbodog",
////        "brewery": "Brewery 11",
////            "beerType": "Wheat",
////                "rate": 4,
////                    "stand": "B",
////                        "price": 7,
////                            "quantityPercen": 98.75
////}

//var searchViewModel = function (searchService) {
//    this.items = ko.observableArray([]);
//    this.beer = "";
//    this.brewery = "";

//    window.items = this.items;

//    this.searchService = searchService;

//    this.sortByName = function () {
//        this.items.sort(function (a, b) {
//            return a.name < b.name ? -1 : 1;
//        });
//    };

//    this.jumpToFirstPage = function () {
//        this.gridViewModel.currentPageIndex(0);
//    };

//    this.search = function () {
//        console.log(this.beer);
//        this.searchService.search('beer', {
//            search: this.beer, top: 10, facets: []
//        }, function (err, results) {
//            console.log(err);
//            console.log(results);
//            window.items.removeAll();
//            results.forEach(function (x) { x => window.items.add(x) })
//            //this.items = ko.observableArray(results);
//            //ko.applyBindings(this);
//        })
//    };
    

//    this.gridViewModel = new ko.simpleGrid.viewModel({
//        data: this.items,
//        columns: [
//            { headerText: "Beer", rowText: "beerName" },
//            { headerText: "Brewery", rowText: "brewery" },
//            { headerText: "Price", rowText: function (item) { return "$" + item.price.toFixed(2) } }, 
//            { headerText: "Type", rowText: "beerType" }, 
//            { headerText: "Rate", rowText: "rate" }, 
//            { headerText: "Stand", rowText: "stand" }, 
//            { headerText: "Avaible Liters", rowText: "quantityPercen"}
//        ],
//        pageSize: 4
//    });
//};
//ko.applyBindings(new searchViewModel(client));