var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, leagueservice) {
    console.log(leagueservice.getLeagues());
}]);

main.factory("leagueservice", function ($http) {
    var allLeagues;
    return {
        getLeagues: function () {
            $http.get("/League/JsonLeagues")
                .then(function (object) {
                    allLeagues = object.data;
                });
            return allLeagues;
        }
    }
});