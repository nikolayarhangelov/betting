$(function () {
    $("#__ig_wm__").remove();
    var people = new $.ig.RESTDataSource({
        dataSource: "/api/people",
        primaryKey: "Id",
        restSettings: {
            create: {
                url: "/api/people"
            },
            update: {
                url: "/api/people"
            },
            remove: {
                url: "/api/people"
            }
        }
    });
    var gridPeople = $("#gridPeopele").igGrid({
        caption: "Хора",
        dataSource: people,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            headerText: "Име",
            key: "Name",
            dataType: "string"
        }],
        features: [{
            name: "Updating",
            editMode: "row",
            startEditTriggers: "dblclick",
            enableAddRow: true,
            enableDeleteRow: true,
            rowEditDialogContainment: "owner",
            columnSettings: [{
                columnKey: "Id",
                readOnly: true
            }, {
                columnKey: "Name",
                editorType: "text",
                editorOptions: {
                    required: true
                }
            }]
        }]
    });
    gridPeople.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridPeople.igGrid("saveChanges");
    });

    var races = new $.ig.RESTDataSource({
        dataSource: "/api/races",
        primaryKey: "Id",
        restSettings: {
            create: {
                url: "/api/races"
            },
            update: {
                url: "/api/races"
            },
            remove: {
                url: "/api/races"
            }
        }
    });
    var gridRaces = $("#gridRaces").igGrid({
        caption: "Състезания",
        dataSource: races,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            headerText: "Име",
            key: "Name",
            template: "<span data-id='${Id}' data-name='${Name}' class='details ui-icon ui-icon-info'></span><span>${Name}</span>",
            dataType: "string"
        }],
        features: [{
            name: "Updating",
            editMode: "row",
            startEditTriggers: "dblclick",
            enableAddRow: true,
            enableDeleteRow: true,
            rowEditDialogContainment: "owner",
            columnSettings: [{
                columnKey: "Id",
                readOnly: true
            }, {
                columnKey: "Info",
                readOnly: true
            }, {
                columnKey: "Name",
                editorType: "text",
                editorOptions: {
                    required: true
                }
            }]
        }]
    });
    gridRaces.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridRaces.igGrid("saveChanges");
    });
    gridRaces.on("click", ".details", function () {
        var raceId = parseInt($(this).attr("data-id")),
            raceName = $(this).attr("data-name");
        showDetails(raceId, raceName);
    });
});

function showDetails(raceId, raceName) {
    if ($("#gridCompetitors").data("igGrid") !== undefined) {
        $("#gridCompetitors").igGrid("destroy");
        $("#gridCompetitors").off();
    }
    if ($("#gridBets").data("igGrid") !== undefined) {
        $("#gridBets").igGrid("destroy");
        $("#gridBets").off();
    }
    if ($("#gridResults").data("igGrid") !== undefined) {
        $("#gridResults").igGrid("destroy");
        $("#gridResults").off();
    }

    $("#gridResults").append("<div class='text-center'><button type='button' class='calculate btn btn-default'>Изчисли Точки</button></div>");

    var competitors = new $.ig.RESTDataSource({
        dataSource: "/api/race/" + raceId + "/lists",
        primaryKey: "Id",
        restSettings: {
            create: {
                url: "/api/racelists"
            },
            update: {
                url: "/api/racelists"
            },
            remove: {
                url: "/api/racelists"
            }
        }
    });
    competitors.dataBind();
    var bets = new $.ig.RESTDataSource({
        dataSource: "/api/race/" + raceId + "/bets",
        primaryKey: "Id",
        restSettings: {
            create: {
                url: "/api/racebets"
            },
            update: {
                url: "/api/racebets"
            },
            remove: {
                url: "/api/racebets"
            }
        }
    });
    bets.dataBind();
    var people = new $.ig.RESTDataSource({
        dataSource: "/api/people",
        primaryKey: "Id"
    });
    people.dataBind();

    var gridCompetitors = $("#gridCompetitors").igGrid({
        width: "100%",
        caption: "Стартов списък - " + raceName,
        dataSource: competitors,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            key: "RaceId",
            dataType: "number",
            hidden: true
        }, {
            key: "Position",
            headerText: "Позиция",
            width: "30%",
            dataType: "number"
        }, {
            key: "PersonId",
            width: "70%",
            headerText: "Състезател",
            dataType: "number",
            formatter: function (val) {
                var peopleData = people.data();
                for (var i = 0; i < peopleData.length; i++) {
                    if (peopleData[i].Id === val) {
                        return peopleData[i].Name;
                    }
                }
                return val;
            }
        }],
        features: [{
            name: "Updating",
            editMode: "row",
            startEditTriggers: "dblclick",
            enableAddRow: true,
            enableDeleteRow: true,
            rowEditDialogContainment: "owner",
            columnSettings: [{
                columnKey: "RaceId",
                dataType: "number",
                defaultValue: raceId
            }, {
                columnKey: "Position",
                dataType: "number",
                editorType: "numeric",
                defaultValue: 1,
                editorOptions: {
                    minValue: 1,
                    defaultValue: 1,
                    button: "spin",
                    required: true
                }
            }, {
                columnKey: "PersonId",
                dataType: "number",
                editorType: "combo",
                editorOptions: {
                    dataSource: people,
                    mode: "dropdown",
                    textKey: "Name",
                    valueKey: "Id",
                    required: true
                }
            }]
        }, {
            name: "Sorting",
            type: "local",
            columnSettings: [{
                columnKey: "Position",
                currentSortDirection: "ascending"
            }]
        }]
    });

    var gridBets = $("#gridBets").igGrid({
        width: "100%",
        caption: "Залози - " + raceName,
        dataSource: bets,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            key: "RaceId",
            dataType: "number",
            hidden: true
        }, {
            key: "Position",
            width: "30%",
            headerText: "Позиция",
            dataType: "number"
        }, {
            key: "PersonId",
            width: "35%",
            headerText: "Заложил",
            dataType: "number",
            formatter: function (val) {
                var peopleData = people.data();
                for (var i = 0; i < peopleData.length; i++) {
                    if (peopleData[i].Id === val) {
                        return peopleData[i].Name;
                    }
                }
                return val;
            }
        }, {
            key: "RaceListId",
            width: "35%",
            headerText: "Състезател",
            dataType: "number",
            formatter: function (val) {
                var competitorsData = competitors.data(),
                    peopleData = people.data(),
                    personId = -1;
                for (var i = 0; i < competitorsData.length; i++) {
                    if (competitorsData[i].Id === val) {
                        personId = competitorsData[i].PersonId;
                        break;
                    }
                }
                for (var i = 0; i < peopleData.length; i++) {
                    if (peopleData[i].Id === personId) {
                        return peopleData[i].Name;
                    }
                }
                return val;
            }
        }],
        features: [{
            name: "Updating",
            editMode: "row",
            startEditTriggers: "dblclick",
            enableAddRow: true,
            enableDeleteRow: true,
            rowEditDialogContainment: "owner",
            columnSettings: [{
                columnKey: "RaceId",
                dataType: "number",
                defaultValue: raceId
            }, {
                columnKey: "Position",
                dataType: "number",
                editorType: "numeric",
                defaultValue: 1,
                editorOptions: {
                    minValue: 1,
                    defaultValue: 1,
                    button: "spin",
                    required: true
                }
            }, {
                columnKey: "PersonId",
                dataType: "number",
                editorType: "combo",
                editorOptions: {
                    dataSource: people,
                    mode: "dropdown",
                    textKey: "Name",
                    valueKey: "Id",
                    required: true
                }
            }, {
                columnKey: "RaceListId",
                dataType: "number",
                editorType: "combo",
                editorOptions: {
                    dataSource: competitors,
                    mode: "dropdown",
                    textKey: "PersonId",
                    valueKey: "Id",
                    required: true
                }
            }]
        }, {
            name: "Sorting",
            type: "local",
            columnSettings: [{
                columnKey: "Position",
                currentSortDirection: "ascending"
            }]
        }, {
            name: "GroupBy",
            type: "local",
            columnSettings: [{
                columnKey: "PersonId",
                isGroupBy: true
            }]
        }]
    });

    gridCompetitors.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridCompetitors.igGrid("saveChanges", function () {
            competitors.dataBind();
            gridBets.igGrid("commit");
            gridBets.igGrid("dataBind");
        });
    });
    gridBets.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridBets.igGrid("saveChanges", function () {
            gridBets.igGrid("commit");
            gridBets.igGrid("dataBind");
        });
    });

    $(".calculate").click(function() {
        calculateResults(raceId, raceName);
    });
}

function calculateResults(raceId, raceName) {
    $("#gridScore").igGrid({
        width: "100%",
        caption: "Резултат - " + raceName,
        dataSource: new $.ig.RESTDataSource({
            dataSource: "/api/race/" + raceId + "/score",
            primaryKey: "Id"
        }),
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            headerText: "Име",
            width: "70%",
            key: "Name",
            dataType: "string"
        }, {
            headerText: "Точки",
            width: "30%",
            key: "Score",
            dataType: "number"
        }],
        features: [{
            name: "Sorting",
            type: "local",
            columnSettings: [{
                 columnKey: "Score",
                 currentSortDirection: "descending"
            }]
        }]
    });
}