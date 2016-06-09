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

main.controller("profilecontroller", function ($scope, profiles) {
    $scope.myinvites;
    profiles.GetAllUsersTeamInvites().then(function (data) {
        $scope.myinvites = data.data;
        console.log($scope.myinvites);
    });

    $scope.acceptinvite = function (invite) {
        console.log("ID of invite", invite.TeamInviteID)
        profiles.AcceptTeamInvite(invite.TeamInviteID).then(function (data) {
            console.log("Data", data);
            invite.Accepted = true;
            console.log(invite);
        });
    }
    $scope.declineinvite = function (id) {
        console.log("You've declined this team invite!")
    }
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
});

main.service("profiles", function ($http) {
    this.AcceptTeamInvite = function (tid) {
        return $http.post('/Profile/AcceptInvite', { id: tid });
    }

    this.GetAllUsersTeamInvites = function () {
        return $http.get('/Profile/GetInvitesForLoggedInUser');
    }
})

