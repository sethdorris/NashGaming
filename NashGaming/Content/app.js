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
    $scope.myteam;
    $scope.ladders;
    $scope.leagues;
    $scope.matches;

    profiles.PopulateProfile().then(function (data) {
        console.log("Profile Data", data.data);
        $scope.myinvites = data.data.Invites;
        $scope.myteam = data.data.TeamInfo;
        $scope.ladders = data.data.Ladders;
        $scope.leagues = data.data.Leagues;
        $scope.Matches = data.data.Matches;
    })

    $scope.acceptinvite = function (invite) {
        console.log("ID of invite", invite.TeamInviteID)
        profiles.AcceptTeamInvite(invite.TeamInviteID).then(function (data) {
            console.log("Data", data);
            invite.Accepted = true;
            console.log(invite);
        });
    }
    $scope.declineinvite = function (invite) {
        profiles.DeleteTeamInvite(invite.TeamInviteID).then(function (data) {
            var indextocut = $scope.myinvites.indexOf(invite);
            $scope.myinvites.splice(indextocut, 1);
        })
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

    this.DeleteTeamInvite = function (tid) {
        return $http.post('/Profile/DeclineInvite', { id: tid });
    }

    this.PopulateProfile = function () {
        return $http.get('Profile/PopulateProfile');
    }
})

