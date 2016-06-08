var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, leagues) {
    console.log("FOOF")
    $scope.leagues;
    leagues.getleagues().then(function (data) {
        $scope.leagues = data.data;
        console.log($scope.leagues)
    })
    leagues.editLeagueGPW().then(function (data) {
        console.log(data.data);
    })
    leagues.getSingleLeague().then(function (data) {
        console.log(data.data);
    })
});

main.controller("profilecontroller", function () {
    console.log("Profile Controller Bitch!")
})

main.service("leagues", function ($http) {

    this.getleagues = function () {
        return $http.get('/League/JsonLeagues');
    };

    this.editLeagueGPW = function () {
        return $http.post('/League/EditGamesPerWeek', { lid: 28, gpw: 6 })
    };

    this.getSingleLeague = function () {
        return $http.post('/League/JsonGetLeagueById', { id: 28 });
    }  
})

