$(function () {
    $("#__ig_wm__").remove();
    var gridPeople = $("#gridPeopele").igGrid({
        caption: "People",
        dataSource: new $.ig.RESTDataSource({
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
        }),
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

    var gridRaces = $("#gridRaces").igGrid({
        caption: "Races",
        dataSource: new $.ig.RESTDataSource({
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
        }),
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
        showDetails($(this).attr("data-id"));
    });
});

function showDetails(raceId) {
    var gridCompetitors = $("#gridCompetitors").igGrid({
        caption: "Competitors",
        dataSource: new $.ig.RESTDataSource({
            dataSource: "/api/race/" + raceId,
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
        }),
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
            headerText: "Competitor",
            key: "PersonId",
            dataType: "number",
            template: "${Person.Name}"
        }, {
            headerText: "Person",
            key: "Person",
            dataType: "object",
            hidden: true
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
                columnKey: "RaceId",
                dataType: "number",
                defaultValue: raceId,
                hidden: true,
                unbound: true
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
                    dataSource: new $.ig.RESTDataSource({
                        dataSource: "/api/people",
                        primaryKey: "Id"
                    }),
                    mode: "dropdown",
                    valueKey: "Id",
                    textKey: "Name",
                    required: true
                }
            }, {
                columnKey: "Person",
                dataType: "object",
                readOnly: true
            }]
        }, {
            name: "Sorting",
            type: "local",
            columnSettings: [{
                columnKey: "Position",
                allowSorting: false,
                currentSortDirection: "ascending"
            }, {
                columnKey: "PersonId",
                allowSorting: false
            }]
        }]
    });
    gridCompetitors.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function () {
        gridCompetitors.igGrid("saveChanges");
    });
}