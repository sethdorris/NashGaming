var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, leagues) {
    $scope.leagues;
    leagues.getleagues().then(function (data) {
        $scope.leagues = data.data;
    })
});

main.service("leagues", function ($http) {

    this.getleagues = function () {
        return $http.get('/League/JsonLeagues');
    };
    
})