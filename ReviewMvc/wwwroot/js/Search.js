$(document).ready(()=> { 
    $("#formId").on('submit',e=> {
        e.preventDefault();
        const query = $('#queryId').val();
        $('tbody').load('/Ratings/Searched?query=' + query)
    });
})