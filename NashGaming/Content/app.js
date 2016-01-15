var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, $http) {
    $http.get("/League/JsonLeagues")
        .then(function (data) {
            $scope.leaguenames = data.data;
        });
});