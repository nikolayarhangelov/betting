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
            dataType: "string"
        }],
        features: [{
            name: "Selection",
            mode: "row",
            activation: true
        },{
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
    gridRaces.on("iggridselectionrowselectionchanged", function (evt, ui) {
        var race = races.data()[ui.row.index];
        showDetails(race.Id, race.Name);
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
            dataType: "number"
        }, {
            key: "PersonId",
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
    gridCompetitors.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridCompetitors.igGrid("saveChanges");
    });

    var gridBets = $("#gridBets").igGrid({
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
            headerText: "Позиция",
            dataType: "number"
        }, {
            key: "PersonId",
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
    gridBets.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridBets.igGrid("saveChanges");
    });
}