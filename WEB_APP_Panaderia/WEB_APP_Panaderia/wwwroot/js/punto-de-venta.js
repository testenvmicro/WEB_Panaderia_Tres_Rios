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
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-nombre="${producto.nombre}" data-producto-precio="${producto.precioUnitario}" data-producto-tipo="${producto.tipo}">
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

        // Añadir el evento de clic para los productos
        $(".product-info").click(function () {
            const productoId = $(this).data('producto-id');
            const productoTipo = $(this).data('producto-tipo');
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId, $(this).data('producto-nombre'), $(this).data('producto-precio'));
            } else {
                addProductToCart($(this).data('producto-nombre'), $(this).data('producto-tipo'), $(this).data('producto-precio'));
            }
        });
    }

    function renderProductosPizzeria(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-nombre="${producto.nombre}" data-producto-precio="${producto.precioUnitario}" data-producto-tipo="${producto.tipo}">
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
                openModal(productoId, $(this).data('producto-nombre'), productoTipo, $(this).data('producto-precio'));
            } else {
                addProductToCart($(this).data('producto-nombre'), $(this).data('producto-tipo'), $(this).data('producto-precio'));
            }
        });
    }

    function renderProductosPastas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-nombre="${producto.nombre}" data-producto-precio="${producto.precioUnitario}" data-producto-tipo="${producto.tipo}">
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

        $(".product-info").click(function () {
            const productoId = $(this).data('producto-id');
            const productoTipo = $(this).data('producto-tipo');
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId, $(this).data('producto-nombre'), $(this).data('producto-precio'));
            } else {
                addProductToCart($(this).data('producto-nombre'), $(this).data('producto-tipo'), $(this).data('producto-precio'));
            }
        });
    }

    function renderProductosBebidas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card" data-producto-id="${producto.idProducto}" data-producto-nombre="${producto.nombre}" data-producto-precio="${producto.precioUnitario}" data-producto-tipo="${producto.tipo}" data-producto-descripcion="${producto.descripcion}">
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

        $(".product-info").click(function () {
            const productoId = $(this).data('producto-id');
            const productoTipo = $(this).data('producto-tipo');
            const productoDescripcion = $(this).data('producto-descripcion');
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId, $(this).data('producto-nombre'), $(this).data('producto-precio'));
            } else {
                addProductToCart($(this).data('producto-nombre'), $(this).data('producto-tipo'), $(this).data('producto-precio'), productoDescripcion); 
            }
        });
    }
    // Función para abrir el modal y cargar el contenido dinámico
    function openModal(productoId, productoNombre, productoTipo, productoPrecio) {
        $.ajax({
            url: '/SaboresPizza/ConsultarSaboresPizza', 
            method: 'GET',
            success: function (response) {
                renderModalContent(response);
                $('#tipo-pizza').modal('show'); // Mostrar el modal

                // Añadir el evento de clic al botón de aceptar en el modal
                $("#accept-button").off('click').on('click', function () {
                    let selectedSabores = $(".sabor-check:checked").map(function () {
                        return $(this).closest('.row').find('h6').text();
                    }).get();
                    addProductToCart(productoNombre, productoTipo, productoPrecio, selectedSabores.join(', '));
                });

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
                                <input type="checkbox" id="tipo${sabor.idSaborPizza}" class="check sabor-check" data-sabor-id="${sabor.idSaborPizza}">
                                <label for="tipo${sabor.idSaborPizza}" class="checktoggle"></label>
                            </div>
                        </div>
                    </div>
                </div>

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

    // Función para agregar productos al carrito
    function addProductToCart(nombre, tipo, precio, descripcion, sabores) {
        let descripcionHTML = descripcion ? `<p>Descripcion: ${descripcion}</p>` : '';
        let saboresHTML = sabores ? `<p>Sabores: ${sabores}</p>` : '';
       
        // Buscar si el producto ya está en la lista
        let existingProduct = $(`.product-list[data-nombre="${nombre}"][data-tipo="${tipo}"][data-sabores="${sabores}"]`);
        if (existingProduct.length > 0) {
            // Incrementar la cantidad del producto existente
            let qtyInput = existingProduct.find(".qty-input");
            let currentQty = parseInt(qtyInput.val());
            let newQty = currentQty + 1;
            qtyInput.val(newQty);
            updateSubtotal(existingProduct, newQty, precio);
        } else {
            let productHTML = `
                            <div class="product-list d-flex align-items-center justify-content-between">
                                <div class="d-flex align-items-center product-info">
                                    <div class="info">
                                        <h6><a href="javascript:void(0);">${nombre}</a></h6>
                                        <p>${tipo}</p>
                                         ${descripcionHTML}
                                        ${saboresHTML}
                                        <p class="subtotal">Subtotal: ₡<span class="subtotal-amount">${precio}</span></p>
                                    </div>
                                </div>
                                <div class="qty-item text-center">
                                    <a href="javascript:void(0);" class="dec d-flex justify-content-center align-items-center" data-bs-toggle="tooltip" data-bs-placement="top" title="minus"><i data-feather="minus-circle" class="feather-14"></i></a>
                                    <input type="text" class="form-control text-center qty-input" name="qty" value="1" readonly>
                                    <a href="javascript:void(0);" class="inc d-flex justify-content-center align-items-center" data-bs-toggle="tooltip" data-bs-placement="top" title="plus"><i data-feather="plus-circle" class="feather-14"></i></a>
                                </div>
                                <div class="d-flex align-items-center action">
                                    <a class="btn-icon delete-icon confirm-text" href="javascript:void(0);">
                                        <i data-feather="trash-2" class="feather-14"></i>
                                    </a>
                                </div>
                            </div>
                            `;
            $("#product-list").append(productHTML);
            feather.replace(); // Actualiza los iconos de Feather
        }
       
        

        // Añadir manejadores de eventos para los botones de incremento y decremento
        $(".inc").off('click').on('click', function () {
            let qtyInput = $(this).siblings(".qty-input");
            let currentQty = parseInt(qtyInput.val());
            let newQty = currentQty + 1;
            qtyInput.val(newQty);
            updateSubtotal($(this).closest('.product-list'), newQty, precio);
            updateOrderTotal();
        });

        $(".dec").off('click').on('click', function () {
            let qtyInput = $(this).siblings(".qty-input");
            let currentQty = parseInt(qtyInput.val());
            if (currentQty > 1) {
                let newQty = currentQty - 1;
                qtyInput.val(newQty);
                updateSubtotal($(this).closest('.product-list'), newQty, precio);
                updateOrderTotal();
            }
        });

        // Añadir manejador de evento para el botón de eliminar
        $(".delete-icon").off('click').on('click', function () {
            $(this).closest('.product-list').remove();
            updateOrderTotal();
        });

        // Actualizar el total de la orden
        updateOrderTotal();

    }

    // Función para actualizar el subtotal
    function updateSubtotal(productElement, quantity, price) {
        let newSubtotal = quantity * price;
        productElement.find(".subtotal-amount").text(newSubtotal);
    }

    // Función para actualizar el total de la orden
    function updateOrderTotal() {
        let total = 0;
        $(".subtotal-amount").each(function () {
            total += parseFloat($(this).text());
        });
        $(".order-total .text-end").html(`₡${total.toFixed(2)}`);
    }

    // Manejador de clic para remover todos los productos
    $("#remove-all").click(function () {
        $("#product-list").empty();
        updateOrderTotal();
    });




});
