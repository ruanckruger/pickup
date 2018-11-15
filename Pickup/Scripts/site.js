var loginOpen = false;
var modalId = "#modal-container";
$("#login-button").click(function (e) {
    $(modalId).remove();
    $.get("/Account/Login", function (data) {
        $("body").prepend(data);
            loginOpen = true;
    });
});
$("#register-button").click(function (e) {
    $(modalId).remove();
    $.get("/Account/Register", function (data) {
        $("body").prepend(data);
        loginOpen = true;
    });
});
$("#account-image").click(function (e) {
    $("#login-modal").remove();
    $.get("/Manage/Index", function (data) {
        $("body").prepend(data);
        loginOpen = true;
    });
});

$("#create-new-match").click(function () {
    console.log("Create clicked");
    $(modalId).remove();
    $.get("/Matches/CreateMatch", function (data) {
        $("body").prepend(data);
        loginOpen = true;
    });
});

$("body").on("click", modalId + " .close-login", function () {
    console.log("close clicked");
    $(modalId).fadeOut();
    loginOpen = false;
});

// #region SignalR
$(function () {
    // Reference the auto-generated proxy for the hub.  
    var game = $.connection.gameHub;
    // Create a function that the hub can call back to display messages.
    // #region Recieves
    game.client.userJoined = function (matchId, userId, newCount) {
        $.get('/Matches/PlayerDisplay?playerId=' + userId + '&matchid=' + matchId, function (data, status) {
            $("#match-" + matchId).find(".match-player-count").text(newCount);
            $("#match-" + matchId).find(".curPlayers").append(data);
        });
            gameReady();
        if (newCount == 10) {

            gameReady();
        };
    };
    game.client.userLeft = function (matchId, userId, newCount) {
        $("#" + userId).remove();
        $("#match-" + matchId).find(".match-player-count").text(newCount);
    };
    game.client.gameCreated = function (matchId) {       
        $.get('/Matches/MatchInfo?matchid=' + matchId, function (data, status) {
            $("#matches-container").append(data);
        });
    };
    game.client.gameEnded = function (matchId) {
        $("#match-" + matchId).remove();
    };
    let gameReady;
    // #endregion

    // #region Sends
    $.connection.hub.start().done(function () {
        $('body').on("click", ".join-match",function () {
            $(".leave-match").addClass("join-match").removeClass("leave-match").text("Join Match");
            game.server.leave();
            game.server.join($(this).attr("match-id"));
            $(this).addClass("leave-match").removeClass("join-match").text("Leave Match");            
        });

        $('body').on("click", ".leave-match", function () {
            console.log("Leave clicked");
            game.server.leave();
            $(this).addClass("join-match").removeClass("leave-match").text("Join Match");
        });

        $("#logout-button").click(function () {
            $("#" + $(this).attr("user-id")).remove();
            game.server.leave();
            $('#logoutForm').submit();
        });

        $("body").on("click", "#createMatchSubmit", function (e) {
            e.preventDefault();
            game.server.createGame($("#MatchMap").val());
            $("#modal-container").remove();
        });

        $("body").on("click", ".end-match", function (e) {
            e.preventDefault();
            game.server.endGame($(this).attr("match-id"));
        });

        gameReady = function() {
            //game.server.gameReady($(this).attr("match-id"));
            console.log("Game ready reached!");
        };
    });
    //#endregion
});
// #endregion
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}