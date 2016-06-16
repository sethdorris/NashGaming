var main = angular.module("app", [], function () { });

main.controller("leaguecontroller", function ($scope, leagues) {
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

main.controller("competitionscontroller", function ($scope, $http, competitions) {
    $scope.leagues;
    $scope.ladders;
    $scope.ViewLeaguesButton = function () {
        competitions.getLeagues().then(function (data) {
            $scope.leagues = data.data;
            $scope.leagues.forEach(function (item) {
                item.StartDate = dateConverter(item.StartDate);
                item.EndDate = dateConverter(item.EndDate);
            })
            console.log($scope.leagues)
        })
    }

    $scope.ViewLaddersButton = function () {
        competitions.getLadders().then(function (data) {
            $scope.ladders = data.data;
            console.log($scope.ladders);
        })
    }

    function dateConverter(string) {
        var date = string;
        date = date.split("\(");
        date = date[1].split("\)");
        date = date[0];
        date = new Date(parseInt(date)).toLocaleString();
        return date;
    }
})

main.controller("profilecontroller", function ($scope, profiles) {
    $scope.myinvites;
    $scope.myteam;
    $scope.ladders;
    $scope.leagues;
    $scope.matches;
    $scope.record = {
        wins: 0,
        losses: 0,
        gp: 0,
        wp: 0
    };

    profiles.PopulateProfile().then(function (data) {
        console.log("Profile Data", data.data);
        $scope.myinvites = data.data.Invites;
        $scope.myteam = data.data.TeamInfo;
        $scope.myteam.DateFounded = dateConverter($scope.myteam.DateFounded);
        $scope.ladders = data.data.Ladders;
        $scope.leagues = data.data.Leagues;
        $scope.Matches = data.data.Matches;
        $scope.Matches.forEach(function (item) {
            item.Result == "W" ? $scope.record.wins++ : $scope.record.losses++;
            item.DatePlayed = dateConverter(item.DatePlayed);
            $scope.record.gp++;
        })
        $scope.record.gp = $scope.record.wins + $scope.record.losses;
        $scope.record.wp = Math.floor(($scope.record.wins / $scope.record.gp) * 100);
    })

    function dateConverter(string) {
        var date = string;
        date = date.split("\(");
        date = date[1].split("\)");
        date = date[0];
        date = new Date(parseInt(date)).toLocaleString();
        return date;
    }

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

main.service("competitions", function ($http) {
    this.getLeagues = function () {
        return $http.get('Competitions/JsonLeagues');
    }
    this.getLadders = function () {
        return $http.get('Competitions/GetAllLadders');
    }
})

