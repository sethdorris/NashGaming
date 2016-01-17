var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, $http) {
    $scope.leagues;
    $http.get('/League/JsonLeagues')
    .then(function (data) {
        $scope.leagues = data.data;
        console.log(data.data);
    })
});