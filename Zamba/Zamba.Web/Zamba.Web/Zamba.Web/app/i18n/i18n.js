app.config(
    ["$translateProvider", function ($translateProvider) {
        $translateProvider.translations(
            "es-ar", {
            "index": {
                "title": "Zamba",
                "home": "Inicio",
                "news": "Novedades",
                "search": "Busqueda",
                "results": "Resultados",
                "process": "Procesos",
                "mytasks": "Mis Tareas",
                "insert": "Insertar",
                "RemoveTask": "Finalizar",
                "AddMyTasks": "Iniciar",
                "DefaultFilterTitle": "Otros filtros"
            }
        });

        $translateProvider.translations(
            "en-us", {
            "index": {
                "title": "Zamba",
                "home": "Home",
                "news": "News",
                "search": "Search",
                "results": "Results",
                "process": "Process",
                "mytasks": "My Tasks",
                "insert": "New",
                "RemoveTask": "Remove Task",
                "AddMyTasks": "Add My Tasks",
                "DefaultFilterTitle": "Other filters"
            }
        });

    }]);