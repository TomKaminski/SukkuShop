$("body").delegate("#saveDoc", "click", function () {
    $("#saveDocForm").submit();
});

function SetEditor() {
    $('#Text').ckeditor({ language: 'pl',height : '500px' });
};
