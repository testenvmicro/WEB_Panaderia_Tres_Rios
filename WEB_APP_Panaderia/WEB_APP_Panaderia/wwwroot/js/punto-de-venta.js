$(document).ready(function () {
    $("#panaderia").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=1', 
            method: 'GET',
            success: function (response) {
                renderProductosPanaderia(response);
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });


    $("#pizzeria").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=2', 
            method: 'GET',
            success: function (response) {
                renderProductosPizzeria(response, 'pizzeria');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    $("#pastas").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=3',
            method: 'GET',
            success: function (response) {
                renderProductosPastas(response, 'pastas');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    $("#bebidas").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=4',
            method: 'GET',
            success: function (response) {
                renderProductosBebidas(response, 'bebidas');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    function renderProductosPanaderia(productos) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        });
        $(".tab_content[data-tab='panaderia'] .row").html(content);
    }

    function renderProductosPizzeria(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-tipo="${producto.tipo}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='pizzeria'] .row`).html(content);

        // Añadir el evento de clic para los productos
        $(".product-info").click(function () {
            const productoId = $(this).data('producto-id');
            const productoTipo = $(this).data('producto-tipo');
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId);
            }
        });
    }

    function renderProductosPastas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-tipo="${producto.tipo}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='pastas'] .row`).html(content);
    }

    function renderProductosBebidas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-tipo="${producto.tipo}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='bebidas'] .row`).html(content);
    }
    // Función para abrir el modal y cargar el contenido dinámico
    function openModal(productoId) {
        $.ajax({
            url: '/SaboresPizza/ConsultarSaboresPizza', 
            method: 'GET',
            success: function (response) {
                renderModalContent(response);
                $('#tipo-pizza').modal('show'); // Mostrar el modal
            },
            error: function (error) {
                console.log("Error al obtener sabores de pizza", error);
            }
        });
    }

    

    // Función para renderizar el contenido del modal
    function renderModalContent(sabores) {
        let content = '';
        sabores.forEach(function (sabor) {
            content += `
                <div class="row align-items-center" data-sabor-id="${sabor.idSaborPizza}">
                    <div class="col-sm-6">
                        <div class="setting-info">
                            <h6>${sabor.nombre}</h6>
                            <p>${sabor.descripcion}</p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="localization-select d-flex align-items-center">
                            <div class="status-toggle modal-status d-flex justify-content-between align-items-center me-3">
                                <input type="checkbox" id="tipo${sabor.idSaborPizza}" class="check sabor-check">
                                <label for="tipo${sabor.idSaborPizza}" class="checktoggle"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            `;
        });
        $(".dynamic-content").html(content);

        // Añadir el evento de cambio para los checkboxes
        $(".sabor-check").change(function () {
            let checkedCount = $(".sabor-check:checked").length;
            $(".sabor-check:not(:checked)").prop('disabled', checkedCount >= 2);
            $("#accept-button").prop('disabled', checkedCount < 1);
        });
    }

});
