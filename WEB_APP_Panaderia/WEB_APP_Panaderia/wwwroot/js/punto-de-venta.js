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
       
            let productHTML = `
                            <div class="product-list d-flex align-items-center justify-content-between" data-nombre="${nombre}" data-tipo="${tipo}" data-sabores="${sabores}" data-precio="${precio}" data-descripcion="${descripcion}">
                                <div class="d-flex align-items-center product-info">
                                    <div class="info">
                                        <h6 class="product-name"><a>${nombre}</a></h6>
                                        <p class="product-type">${tipo}</p>
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
        $(document).ready(function () {
            $('#payButton').click(function () {
                // Obtener la información de los productos
                var products = [];
                $('.product-list').each(function () {
                    var productoNombre = $(this).data('nombre');
                    var productoPrecio = $(this).data('precio');
                    var productoTipo = $(this).data('tipo');
                    var productoDescripcion = $(this).data('descripcion');
                    products.push({
                        nombre: productoNombre,
                        precio: productoPrecio,
                        tipo: productoTipo,
                        descripcion: productoDescripcion
                    });
                });

                // Obtener el total (asumiendo que tienes un elemento con el total)
                var total = $('.order-total .text-end').text();

                // Obtener el método de pago seleccionado
                var selectedMethod = $('.payment-method .selected-method span').text();

                // Verificar si el pedido es express
                var isExpress = $('#express-order-checkbox').is(':checked');

                // Obtener la información del cliente si existe
                var customerInfo = '';
                if ($('#customerNameHidden').text() !== '') {
                    customerInfo += 'Nombre del cliente: ' + $('#customerNameHidden').text() + '\n';
                }
                if ($('#customerEmailHidden').text() !== '') {
                    customerInfo += 'Correo: ' + $('#customerEmailHidden').text() + '\n';
                }
                if ($('#customerPhoneHidden').text() !== '') {
                    customerInfo += 'Teléfono: ' + $('#customerPhoneHidden').text() + '\n';
                }
                if ($('#customerProvinceHidden').text() !== '') {
                    customerInfo += 'Provincia: ' + $('#customerProvinceHidden').text() + '\n';
                }
                if ($('#customerCantonHidden').text() !== '') {
                    customerInfo += 'Cantón: ' + $('#customerCantonHidden').text() + '\n';
                }
                if ($('#customerDistrictHidden').text() !== '') {
                    customerInfo += 'Distrito: ' + $('#customerDistrictHidden').text() + '\n';
                }

                // Construir el mensaje para el alert
                var message = 'Productos:\n';
                products.forEach(function (product) {
                    message += 'Nombre: ' + product.nombre + ', Precio: ' + product.precio + ', Tipos: ' + product.tipo + ', Descripcion: ' + product.descripcion + '\n';
                });
                message += '\nTotal: ' + total + '\n';
                message += 'Método de pago: ' + selectedMethod + '\n';
                message += 'Pedido Express: ' + (isExpress ? 'Sí' : 'No') + '\n';
                message += '\nInformación del cliente:\n' + (customerInfo !== '' ? customerInfo : 'N/A');

                // Mostrar la información en un alert
                alert(message);
            });
        });

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

$(document).ready(function () {
    // Manejar el checkbox de pedido express
    $('#express-order-checkbox').change(function () {
        if ($(this).is(':checked')) {
            $('.customer-info.block-section').removeClass('hidden');
        } else {
            $('.customer-info.block-section').addClass('hidden');
        }
    });

    // Manejar el envío del formulario del modal
    $('#create-customer-form').submit(function (event) {
        event.preventDefault(); // Prevenir el envío por defecto del formulario

        // Obtener los valores del formulario
        const nombre = $(this).find('input[name="nombre"]').val();
        const correo = $(this).find('input[name="correo"]').val();
        const telefono = $(this).find('input[name="telefono"]').val();
        const provincia = $(this).find('input[name="provincia"]').val();
        const canton = $(this).find('input[name="canton"]').val();
        const distrito = $(this).find('input[name="distrito"]').val();

        // Guardar la información en elementos ocultos para después usarlos
        $('#customerNameHidden').text(nombre);
        $('#customerEmailHidden').text(correo);
        $('#customerPhoneHidden').text(telefono);
        $('#customerProvinceHidden').text(provincia);
        $('#customerCantonHidden').text(canton);
        $('#customerDistrictHidden').text(distrito);

        // Crear el HTML para mostrar la información del cliente solo si los campos no están vacíos
        let customerInfoHTML = '<div class="customer-details">';
        if (nombre) {
            customerInfoHTML += `<p><strong>Nombre del cliente:</strong> ${nombre}</p>`;
        }
        if (correo) {
            customerInfoHTML += `<p><strong>Correo:</strong> ${correo}</p>`;
        }
        if (telefono) {
            customerInfoHTML += `<p><strong>Teléfono:</strong> ${telefono}</p>`;
        }
        if (provincia) {
            customerInfoHTML += `<p><strong>Provincia:</strong> ${provincia}</p>`;
        }
        if (canton) {
            customerInfoHTML += `<p><strong>Cantón:</strong> ${canton}</p>`;
        }
        if (distrito) {
            customerInfoHTML += `<p><strong>Distrito:</strong> ${distrito}</p>`;
        }
        customerInfoHTML += '</div>';

        // Limpiar la información anterior del cliente
        $('.customer-info.block-section .customer-details').remove();

        // Añadir la nueva información del cliente al div .customer-info.block-section solo si hay información para mostrar
        if (customerInfoHTML !== '<div class="customer-details"></div>') {
            $('.customer-info.block-section').append(customerInfoHTML);
        }

        // Cerrar el modal
        $('#create').modal('hide');

        // Limpiar el formulario
        $('#create-customer-form')[0].reset();
    });

    $(document).ready(function () {
        $('.payment-method .item .default-cover a').click(function () {
            // Remover la clase 'selected-method' de todos los métodos de pago
            $('.payment-method .item .default-cover').removeClass('selected-method');

            // Añadir la clase 'selected-method' al contenedor seleccionado
            $(this).closest('.default-cover').addClass('selected-method');
        });
    });

   


});


