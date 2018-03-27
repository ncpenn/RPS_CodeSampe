$(function () {

    var rootUrl = window.location.protocol + '//' + window.location.host;

    var model = {
        PolicyNumber: null,
        EffectiveDate: null,
        ExpireDate: null,
        FirstName: null,
        LastName: null,
        Street: null,
        City: null,
        State: null,
        Zip: null,
        SelectedRiskConstruction: null,
        YearBuilt:  null,
        BuildingStreet: null,
        BuildingCity: null,
        BuildingState: null,
        BuildingZip: null
    };

    function removeHash() {
        window.location.replace("#");
    }

    $("#addPolicyFormModal").dialog({
        autoOpen: false,
        modal: true,
        width: 700,
        height: 420,
        position: { my: "left top", at: "left bottom", of: $('#addBtn') },
        open: function (event, ui) {
            $('#addPolicyFormModal').css('overflow', 'hidden');
        },
        close: removeHash,
        show: {
            effect: "blind",
            duration: 1000
        }
    });

    function addPolicy(e) {
        e.preventDefault();
        removeHash();
        $(".sw-btn-next").button("disable");
        $(".sw-btn-prev").button("disable");

        var $formFields = $(":text");

        model.SelectedRiskConstruction = $("#SelectedRiskConstruction option:selected").val();
        model.EffectiveDate = $("#start_date").val();
        model.ExpireDate = $("#end_date").val();
        model.YearBuilt = $("#year_built").val();

        $.each($formFields, function (_, item) {
            model[item.name] = item.value;
        });

       
    $.ajax(
        {
            url: rootUrl + '/InsurancePolicy/Add',
            type: "POST",
            data: JSON.stringify(model),
            processData: false,
            contentType: "application/json",
            success: function (data, textStatus, jqXHR) {
                var result = JSON.parse(data);
                if (result.HasError) {
                    handleError(result.ErrorMessage);
                } else {
                    $('#addPolicyFormModal').dialog('close');
                    location.reload();
                }
                enableFormButtons();
            },
            error: function (jqXHR, error) {
                handleError(jqXHR.status, jqXHR.responseText);
                enableFormButtons();
            }
        });
    }

    function handleError(incoming, errorMessage) {
        var msg = '';
        if (incoming === 0) {
            msg = 'Not connect.\n Verify Network.';
        } else if (incoming === 404) {
            msg = 'Requested page not found. [404]';
        } else if (incoming === 500) {
            msg = 'Internal Server Error [500].';
        } else if (errorMessage) {
            msg = errorMessage;
        } else {
            msg = incoming;
        }

        $('#validationError').text(msg);
        $('#validationError').addClass('alert alert-danger');
    }

    function enableFormButtons() {
        $(".sw-btn-next").button("enable");
        $(".sw-btn-prev").button("enable");
    }

    function validateStep(e, anchorObject, stepNumber, stepDirection) {
        var valid = true;

        if (stepDirection === "forward") {
            $(":text:visible").is(function () {
                if ($(this).val().length < 1) {
                    $(this).addClass("ui-state-error");
                    valid = false;
                } else {
                    $(this).removeClass("ui-state-error");
                }
            });
        }

        if (valid && stepDirection === "forward" && stepNumber === 3) {
            var $button = $('.sw-btn-next');
            $button.html("Save")
            $button.button("enable");
            $button.addClass("btn-success");
            $button.on('click', addPolicy)
        } else {
            var $button = $('.sw-btn-next');
            $button.html("Save")
            $button.removeClass("btn-success");
            $button.html("Next")
            $button.off('click', addPolicy )
        }

        return valid;
    }

    $("#addBtn").on("click", function (e) {

        e.preventDefault();

        $("#addPolicyFormModal").html("");
        
        $("#addPolicyFormModal").dialog().dialog("open");
        $("#addPolicyFormModal").dialog("option", "title", "Loading...");
        $("#addPolicyFormModal").load(rootUrl + '/insurancepolicy/add', function () {
            $("#addPolicyFormModal").dialog("option", "title", "");
            $('#smartwizard').smartWizard({
                autoAdjustHeight: false
            });

            $(".ui-dialog-titlebar-close").html("X");

            $("#smartwizard").on("leaveStep", validateStep);

            $("#year_built").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#start_date").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#end_date").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });

    });

});