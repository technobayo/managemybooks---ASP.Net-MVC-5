$(document).ready(function () {

    //total number of books
    var numberOfBooks = $("#books .book").length;

    //number of books to show per page
    var limitPerpage = 6;

    //Show 6 books and hide the rest
    $("#books .book").slice(limitPerpage).hide();

    //get total number of pages - 3
    var totalPages = Math.ceil(numberOfBooks / limitPerpage);

    //Add pagination children - prev, 1
    $(".pagination").append("<li><a href='javascript:void(0)' id='prevPage'>Previous</a></li>");
    $(".pagination").append("<li class='page-item active'><a href='javascript:void(0)'>" + 1 + "</a></li>");

    //loop through to add rest of pagination children - 2, 3
    for (var i = 2; i <= totalPages; i++) {
        $(".pagination").append("<li class='page-item'><a href='javascript:void(0)'>" + i + "</a></li>");
    }

    //Add pagination children - next, 1
    $(".pagination").append("<li><a href='javascript:void(0)' id='nextPage'>Next</a></li>");

    //clicking pagination children 1, 2, 3
    $(".pagination li.page-item").on("click", function () {

        if ($(this).hasClass("active")) {
            return false;
        }
        else {
            //remove the active attribute from pre-existing pagination button
            $(".pagination li").removeClass("active");
            //add to the current pagination button
            $(this).addClass("active");

            //hide all items n show only next 6
            $("#books .book").hide();
            var currentPage = $(this).index();
            var grandTotal = limitPerpage * currentPage;
            for (var i = grandTotal - limitPerpage; i < grandTotal; i++) {
                $("#books .book").slice(i, grandTotal).show();
            }
        }

    });


    //click function for next pagination
    $("#nextPage").on("click", function () {

        //get current page based on the active class attribute
        var currentPage = $(".pagination li.active").index();

        if (currentPage === totalPages) {
            return false;
        }
        else {
            currentPage++;

            $(".pagination li").removeClass("active");
            
            
            $("#books .book").hide();
            
            var grandTotal = limitPerpage * currentPage;
            for (var i = grandTotal - limitPerpage; i < grandTotal; i++) {
                $("#books .book").slice(i, grandTotal).show();
            }

            $(".pagination li.page-item:eq(" + (currentPage - 1)+")").addClass("active");
        }
    });

    //click function for previous page
    $("#prevPage").on("click", function () {

        var currentPage = $(".pagination li.active").index();

        if (currentPage === 1) {
            return false;
        }
        else {
            currentPage--;


             $(".pagination li").removeClass("active");

             $("#books .book").hide();

             var grandTotal = limitPerpage * currentPage;
             for (var i = grandTotal - limitPerpage; i < grandTotal; i++) {
                 $("#books .book").slice(i, grandTotal).show();
             }

             $(".pagination li.page-item:eq(" + (currentPage - 1) + ")").addClass("active");
        }

    });
});