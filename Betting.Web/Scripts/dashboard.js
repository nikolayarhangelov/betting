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
        caption: "People",
        dataSource: people,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            headerText: "Name",
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
        caption: "Races",
        dataSource: races,
        autoGenerateColumns: false,
        primaryKey: "Id",
        columns: [{
            key: "Id",
            dataType: "number",
            hidden: true
        }, {
            key: "Info",
            headerText: "",
            unbound: true,
            template: "<span data-id='${Id}' class='details ui-icon ui-icon-info'></span>"
        }, {
            headerText: "Name",
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

    $(".grid").on("click", ".details", function () {
        showDetails(parseInt($(this).attr("data-id")));
    });
});

function showDetails(raceId) {
    if ($("#gridCompetitors").data("igGrid") !== undefined) {
        $("#gridCompetitors").igGrid("destroy");
        $("#gridCompetitors").off();
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
    var people = new $.ig.RESTDataSource({
        dataSource: "/api/people",
        primaryKey: "Id"
    });
    people.dataBind();
    var gridCompetitors = $("#gridCompetitors").igGrid({
        caption: "Competitors",
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
            headerText: "Position",
            dataType: "number"
        }, {
            key: "PersonId",
            headerText: "Person",
            dataType: "number",
            formatter: function (val) {
                var peopleData = people.data();
                for (var i = 0; i < peopleData.length; i++) {
                    if (peopleData[i].Id === val) {
                        return peopleData[i].Name;
                    }
                }
                return "";
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
                allowSorting: false,
                currentSortDirection: "ascending"
            }, {
                columnKey: "PersonName",
                allowSorting: false
            }]
        }]
    });
    gridCompetitors.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridCompetitors.igGrid("saveChanges");
    });
}