$(function() {
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
        columns: [
            {
                key: "Id",
                dataType: "number",
                hidden: true
            }, {
                headerText: "Name",
                key: "Name",
                dataType: "string"
            }
        ],
        features: [
            {
                name: "Updating",
                editMode: "row",
                enableAddRow: true,
                enableDeleteRow: true,
                rowEditDialogContainment: "owner",
                columnSettings: [
                    {
                        columnKey: "Id",
                        readOnly: true
                    },
                    {
                        columnKey: "Name",
                        editorType: "text",
                        editorOptions: {
                            required: true
                        }
                    }
                ]
            }
        ]
    });
    gridPeople.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function() {
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
        columns: [
            {
                key: "Id",
                dataType: "number",
                hidden: true
            }, {
                headerText: "Name",
                key: "Name",
                dataType: "string"
            }, {
                headerText: "",
                key: "Details",
                dataType: "string",
                unbound: true,
                template: "<input type='button' onclick='showDetails(${Id})' value='Details'/>"
            }
        ],
        features: [
            {
                name: "Updating",
                editMode: "row",
                enableAddRow: true,
                enableDeleteRow: true,
                rowEditDialogContainment: "owner",
                columnSettings: [
                    {
                        columnKey: "Id",
                        readOnly: true
                    },
                    {
                        columnKey: "Name",
                        editorType: "text",
                        editorOptions: {
                            required: true
                        }
                    },
                    {
                        columnKey: "Details",
                        readOnly: true
                    }
                ]
            }
        ]
    });
    gridRaces.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function() {
        gridRaces.igGrid("saveChanges");
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
        columns: [
            {
                key: "Id",
                dataType: "number",
                hidden: true
            }, {
                key: "RaceId",
                dataType: "number",
                hidden: true
            }, {
                headerText: "Competitor",
                key: "PersonId",
                dataType: "number"
            }
        ],
        features: [
            {
                name: "Updating",
                editMode: "row",
                enableAddRow: true,
                enableDeleteRow: true,
                rowEditDialogContainment: "owner",
                columnSettings: [
                    {
                        columnKey: "Id",
                        readOnly: true
                    }, {
                        columnKey: "RaceId",
                        dataType: "number",
                        defaultValue: raceId,
                        hidden: true,
                        unbound: true
                    }, {
                        columnKey: "PersonId",
                        dataType: "number",
                        editorType: "combo",
                        editorOptions: {
                            dataSource: new $.ig.RESTDataSource({
                                dataSource: "/api/people",
                                primaryKey: "Id"
                            }),
                            valueKey: "Id",
                            textKey: "Name",
                            required: true
                        }
                    }
                ]
            }
        ]
    });
    gridCompetitors.on("iggridupdatingeditrowended iggridupdatingrowdeleted", function() {
        gridCompetitors.igGrid("saveChanges");
    });
}