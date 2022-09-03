
var TourismApp = TourismApp || {
    Resources: {
        Language: document.cookie.indexOf("CurrentLanguage=ES") !== -1 ? "ES" : "EN",
        Get: new function (key) {

            let ResourcesEs = function (key) {
                var obj = {
                    "_Item_AddHighlighted": "Añadir a elementos destacados",
                    "_No_Results_For": "Sin resultados para ",
                    "accommodationsUnder": "Hoteles",
                    "AccType": "Tipo de Hotel",
                    "actOfferedBy": "Actividad ofertada por",
                    "actWithRoom": "Habitaciones del hotel",
                    "brandAccommodation": "Marca del Hotel",
                    "hasFacility": "Comodidades",
                    "roomFacility": "Comodidades de la habitación",
                    "hasRestaurant": "Restaurantes",
                    "hasRoom": "Habitaciones",
                    "isLocated": "Localización",
                    "offer": "Ofrece",
                    "offerActivity": "Ofrece las actividades",
                    "offeredBy": "Ofrecido por",
                    "underBrand": "Marca",
                    "amenities": "Comodidades",
                    "awards": "Reconocimientos",
                    "brandCode": "Marca",
                    "code": "Código",
                    "continent": "Continente",
                    "country": "País",
                    "hasStars": "Estrellas",
                    "maxPrice": "Precio máximo",
                    "minPrice": "Precio mínimo",
                    "numberRooms": "Cantidad de habitaciones",
                    "onlyAdults": "Solo Adultos",
                    "visitors": "Visitantes",
                    "AccommodationsByContinents": "Continente",
                    "MealPlan": "Plan alimenticio",
                    "HotelRating": "Calificación",
                    "AccommodationTypes": "Tipo de alojamiento",
                    "AccommodationGroups": "Grupo del alojamiento",
                    "GeneralKnowledge": "Información General",
                    "NaturalAreas": "Tipo",
                    "Beach": "Tipo"
                };

                value = key in obj ? obj[key] : '';

                return value;
            };

            let ResourcesEn = function (key) {
                var obj = {
                    "_Item_AddHighlighted": "Add to highlighted items",
                    "_No_Results_For": "No result for ",
                    "accommodationsUnder": "Accommodations Under",
                    "AccType": "Accommodation Type",
                    "actOfferedBy": "Activity Offered By",
                    "actWithRoom": "Accommodation With Rooms",
                    "brandAccommodation": "Accommodation Brand",
                    "hasFacility": "Facilities",
                    "roomFacility": "Room Facilities",
                    "hasRestaurant": "Restaurants",
                    "hasRoom": "Rooms",
                    "isLocated": "Location",
                    "offer": "Offer",
                    "offerActivity": "Offer Activity",
                    "offeredBy": "Offered By",
                    "underBrand": "Brand",
                    "amenities": "Amenities",
                    "awards": "Awards",
                    "brandCode": "Brand",
                    "code": "Code",
                    "continent": "Continent",
                    "country": "Country",
                    "hasStars": "Stars",
                    "maxPrice": "Max Price",
                    "minPrice": "Min Price",
                    "numberRooms": "Rooms Qty",
                    "onlyAdults": "Only Adults",
                    "visitors": "Visitors",
                    "AccommodationsByContinents": "Continent",
                    "MealPlan": "Meal Plan",
                    "HotelRating": "Rating",
                    "AccommodationTypes": "Accommodation Type",
                    "AccommodationGroups": "Accommodation Group",
                    "GeneralKnowledge": "General",
                    "NaturalAreas": "Type",
                    "Beach": "Type"

                };

                value = key in obj ? obj[key] : '';

                return value;
            };

            return function (key) {
                var isSpanish = document.cookie.indexOf("CurrentLanguage=ES") !== -1;
                if (isSpanish) {
                    return ResourcesEs(key);
                }
                else {
                    return ResourcesEn(key);
                }
            };

        }
    },
    Data: {
        Url: '',
        OntologyBaseChildren: null,
        SearchItems: [],
        Ontology:null,
        OntologyPrefixes: [
            'http://www.semanticweb.org/user/ontologies/2022/0/tourism#',
            'http://www.w3.org/2002/07/owl#',
            '^^http://www.w3.org/2001/XMLSchema#int',
            '^^http://www.w3.org/2001/XMLSchema#double',
            '^^http://www.w3.org/2001/XMLSchema#boolean',
        ]
    },
    Constants: {
        BaseUrl: 'https://localhost:44363/'
    },
    Ajax: new function () {
        let isRequesting = false;
        let token = $("#__AjaxForm [name=__RequestVerificationToken]");
        let doneProcess = function (data, successCallback, errorCallback) {
            isRequesting = false;
            if (data) {
                if (data.targetUrl && data.targetUrl.url) {
                    Swal.close();
                    window.open(data.targetUrl.url, data.targetUrl.external ? '_blank' : '_self');
                    return;
                }
                TourismApp.Message.Close();
                if (data.success && successCallback instanceof Function)
                    successCallback(data);
                else if (!data.success && errorCallback instanceof Function)
                    errorCallback(data);
            } else {
                TourismApp.Message.Close();
            }
        };
        let failProcess = function (data, errorCallback) {
            TourismApp.Message.Close();
            isRequesting = false;

            if (data && errorCallback instanceof Function) {
                errorCallback(data);
            }
        };

        this.Get = function (url, data, successCallback, errorCallback, showLoading) {

            if (isRequesting) return;

            $.ajax({
                type: 'GET',
                url: url,
                beforeSend: function (request) {
                    isRequesting = true;

                    if (showLoading)
                        TourismApp.Message.Loading();

                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());
                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

        this.Post = function (url, data, successCallback, errorCallback, showLoading) {
            if (isRequesting) return;

            $.ajax({
                type: 'POST',
                url: url,
                beforeSend: function (request) {
                    if (showLoading)
                        TourismApp.Message.Loading();
                    isRequesting = true;
                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());

                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

    },

    Message: new function () {
        let imageUrl = "/images/Logos/logo-hexagon.png";
        let width = '40%';
        var calculateSwalWidth = function () {
            let windowWidth = $(window).width();
            if (windowWidth < 800 && windowWidth > 650) {
                width = '30%';
            }
            if (windowWidth < 650) {
                width = '40%';
            }
        };

        this.Loading = function (data) {
            calculateSwalWidth();
            var lang = document.cookie.indexOf("CurrentLanguage=ES") != -1;

            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message
                ? "<h4>" + data.message + "</h4>"
                : lang
                    ? "<h5>procesando solicitud...</h5>"
                    : "<h5>processing request...</h5>";

            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;

            Swal.fire({
                title: modalTitle,
                imageUrl: TourismApp.Constants.BaseUrl + imageUrl,
                html: modalMessage,
                width: width,
                onOpen: () => {
                    Swal.showLoading();
                },
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer
            });
        };

        function typeMessage(type, data) {
            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message ? "<h4>" + data.message + "</h4>" : '';
            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;
            let modalClosable = data && data.isClosable && !data.isClosable ? false : true;

            return {
                title: modalTitle,
                html: modalMessage,
                width: width,
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer,
                type: type,
                showConfirmButton: modalClosable
            };
        }

        function typeConfirmationMessage(type, data) {
            let modalTitle = data && data.title ? data.title : '';
            let modalMessage = data && data.message ? data.message : '';
            let modalType = type ? type : '';
            let cancelBtnText = data && data.cancelBtnText ? data.cancelBtnText : '';
            let acceptBtnText = data && data.acceptBtnText ? data.acceptBtnText : '';

            return {
                title: modalTitle,
                text: modalMessage,
                imageUrl: !modalType ? TourismApp.Constants.BaseUrl + imageUrl : '',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: cancelBtnText,
                confirmButtonText: acceptBtnText
            };
        }

        this.Info = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('info', data));
        };

        this.Success = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('success', data));
        };

        this.Warn = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('warning', data));
        };

        this.Error = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('error', data));
        };

        this.Confirm = function (data, type) {
            calculateSwalWidth();
            Swal.fire({
                title: data.title,
                html: data.message,
                type: type,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: data.cancelBtnText,
                confirmButtonText: data.acceptBtnText
            }).then((result) => {
                if (data) {
                    if (result.value) {
                        if (data.onAccept instanceof Function)
                            data.onAccept();
                    } else
                        if (data.onCancel instanceof Function)
                            data.onCancel();
                }
            });
        };

        this.ExternalRedirect = function (data) {
            calculateSwalWidth();
            Swal.fire({
                title: data.title,
                html: data.message,
                imageUrl: TourismApp.Constants.BaseUrl + imageUrl,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: data.cancelBtnText,
                confirmButtonText: data.acceptBtnText
            }).then((result) => {
                if (data) {
                    if (result.value) {
                        if (data.onAccept instanceof Function)
                            data.onAccept();
                    } else
                        if (data.onCancel instanceof Function)
                            data.onCancel();
                }
            });
        };

        this.Close = function () {
            Swal.close();
        };

        this.ViewBagMessage = function (data) {
            if (data.response && data.response !== "") {
                if (data.status && data.status !== "") {
                    if (data.status === "Success") {
                        return TourismApp.Alert.Message({ message: data.response, type: "success", closeable: true });
                    }
                    else {
                        return TourismApp.Alert.Message({ message: data.response, type: "error", closeable: true });
                    }
                }
                return "";
            }
            return "";
        };

    },

    Alert: new function () {

        this.Message = function (data) {
            let alertMessage = data && data.message ? data.message : '';
            let type = data && data.type === 'error' ?
                'alert-danger' :
                data.type === 'warning' ?
                    'alert-warning' :
                    data.type === 'success' ?
                        'alert-success' :
                        '';;
            let closeable = data && data.closeable ? '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' : '';
            let dismisable = data && data.closeable ? 'alert-dismissible' : '';

            var message = '<div class="alert ' + dismisable + ' ' + type + ' show" role="alert"> <strong>' + alertMessage + '</strong>' + closeable + ' </div>';
            return message;
        };

        this.ValidationError = function (data) {
            let type = data && data.type === 'error' ?
                'text-danger' :
                data.type === 'warning' ?
                    'text-warning' :
                    data.type === 'success' ?
                        'text-success' :
                        '';;
            var message = "<span class = '" + type + "'>" + data.message + "</span>";
            return message;
        };
    },

    Helper: new function () {
        
        this.ReplaceOntology = function (str) {
            if (str === '' || str == undefined)
                return str;

            const output = str.replace(TourismApp.Data.OntologyPrefixes[0], '')
                .replace(TourismApp.Data.OntologyPrefixes[1], '')
                .replace(TourismApp.Data.OntologyPrefixes[2], '')
                .replace(TourismApp.Data.OntologyPrefixes[3], '')
                .replace(TourismApp.Data.OntologyPrefixes[4], '')
                .replaceAll('_', ' ')
                .replace('%E2%80%99', /'/g)
                .replace('%C3%A1', 'á');

            return output;
        };

        this.ReplaceBaseOntology = function (str) {
            if (str === '' || str == undefined)
                return str;

            const output = str.replace(TourismApp.Data.OntologyPrefixes[0], '')
                .replace(TourismApp.Data.OntologyPrefixes[1], '')
                .replace(TourismApp.Data.OntologyPrefixes[2], '')
                .replace(TourismApp.Data.OntologyPrefixes[3], '')
                .replace(TourismApp.Data.OntologyPrefixes[4], '');

            return output;
        };

        this.Filter = function(data, keys) {
            for (var i in data) {
                if (keys.indexOf(i) != -1) {
                    delete data[i];
                } else if (i === "properties") {
                    for (var j in data.properties) {
                        data.properties[j] = filter(data.properties[j])
                    }
                }
                else if (i === "axioms") {
                    for (var j in data.children) {
                        data.axioms[j] = filter(data.axioms[j])
                    }
                }
                else if (i === "values") {
                    for (var j in data.children) {
                        data.values[j] = filter(data.values[j])
                    }
                }
            }
            return data;
        }
    }
};

$(document).on('click', '.loading', function () {
    TourismApp.Message.Loading();
});

function setSearchItems() {

    TourismApp.Ajax.Get(TourismApp.Data.Url, {},
        function (datos) {
            if (datos.result.response != null && datos.result.response != null) {
                TourismApp.Data.SearchItems = datos.result.response;
                InitializeTypeAhead();
            }
        }, function (datos) { }, false);
}

function InitializeTypeAhead() {
    $('.js-typeahead-items').typeahead({
        minLength: 1,
        maxItem: 15,
        href: TourismApp.Constants.BaseUrl + "Destinations/DestinationDetail/?type={{parent}}&destination={{class}}&city={{city}}",
        template: "{{city}} <small style='color:#999;'>{{class}}</small>",
        order: "asc",
        offset: true,
        emptyTemplate: TourismApp.Resources.Get("_No_Results_For")  + '"{{query}}"',
        display: ["city"],
        source: {
            data: TourismApp.Data.SearchItems
        },
        callback: {
            onClickAfter: function (node, a, item, event) {
                event.preventDefault();
                window.open(item.href);
            },
            onResult: function (node, query, result, resultCount, resultCountPerGroup) {
                const word = query.replace(/ /g, '');

                if (Number(resultCount) == 0 && word == '') {
                    $('.typeahead__list .typeahead__item ').remove();
                }
            }
        }
    });
    $('.js-typeahead-items').removeAttr('disabled');
}

function createVueInstance(ontolData) {
    TourismApp.Data.Ontology = JSON.parse(ontolData);
    const app = Vue.createApp({
        data() {
            return {
                ontology: JSON.parse(ontolData),
                filteredOntology: null,
                search: {
                    prev: '',
                    new: ''
                }
            }
        },
        mounted: function () {
            this.filteredOntology = this.ontology;
        },
        methods: {
            clickCallback: function (pageNum) {
                this.pagination.currentPage = Number(pageNum);
            },
            replaceOntology: function (str) {
                return TourismApp.Helper.ReplaceOntology(str);
            },
            replaceBaseOntology: function (str) {
                return TourismApp.Helper.ReplaceBaseOntology(str);
            },
            resources: function (str) {
                return TourismApp.Resources.Get(str);
            }
        },
        computed: {
            getOntologyItem: function () {
                const isSearch = this.search.new != '';

                if (this.search.new != this.search.prev) {
                    if (isSearch) {
                        const individuals = this.ontology[0].individuals.filter(
                            (entry) => this.ontology[0].individuals.length
                                ? ['individualName', 'valueList']
                                    .some(key => ('' + TourismApp.Helper.ReplaceOntology(entry[key])).toLowerCase().includes(this.search.new.toLowerCase()))
                                : true
                        );

                        var ontol = [{
                            class: this.ontology[0].class,
                            parent: this.ontology[0].parent,
                            comment: this.ontology[0].comment,
                            children: this.ontology[0].children,
                            individuals: individuals
                        }];

                        this.filteredOntology = ontol;
                    }
                    
                    this.search.prev = isSearch ? this.search.new : '';
                }
                 
                if (this.search.new == '')
                    this.filteredOntology = this.ontology;

                return this.filteredOntology;
            }
        },
    }).use("VuejsPaginateNext");

    app.mount("#app");
}