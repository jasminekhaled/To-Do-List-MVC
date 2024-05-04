$(document).ready(function ()
{
    $('#DeleteBtn').on('click', function () {
        var btn = $(this);
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#f3969a",
            cancelButtonColor: "#6cc3d5",
            confirmButtonText: "delete!"
        })
        .then((result) =>
        {
            if (result.isConfirmed)
            {
                $.ajax({
                    url: `/Missions/DeleteMission/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function()
                    {
                        Swal.fire
                        ({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function()
                    {
                        Swal.fire
                        ({
                            title: "OOOPS!",
                            text: "Something Went wrong!",
                            icon: "error"
                        });
                    }

                })
            }
        });
    })



    $('#CompleteBtn').on('click', function () {
        var btn = $(this);
        Swal.fire({
            title: "Do you want to mark it as InCompleted?",
            showCancelButton: true,
            confirmButtonColor: "f3969a",
            cancelButtonColor: "#6cc3d5",
            confirmButtonText: "Do it!"
        })
            .then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/Missions/CompleteMission/${btn.data('id')}`,
                        method: 'Put',
                        success: function () {
                            Swal.fire
                                ({
                                    title: "Done!",
                                    text: "Your Task is marked as InCompleted Successfully",
                                    icon: "success"
                                });
                            window.location.href = '/Missions/Index';
                        },
                        error: function () {
                            Swal.fire
                                ({
                                    title: "OOOPS!",
                                    text: "Something Went wrong!",
                                    icon: "error"
                                });
                        }

                    })
                }
            });
    })


    $('#InCompleteBtn').on('click', function () {
        var btn = $(this);
        Swal.fire({
            title: "Do you want to mark it as Completed?",
            showCancelButton: true,
            confirmButtonColor: "#f3969a",
            cancelButtonColor: "#6cc3d5",
            confirmButtonText: "Do it!"
        })
            .then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/Missions/CompleteMission/${btn.data('id')}`,
                        method: 'Put',
                        success: function () {
                            Swal.fire
                                ({
                                    title: "Done!",
                                    text: "Your Task is marked as Completed Successfully",
                                    icon: "success"
                                });
                            window.location.href = '/Missions/Index';
                        },
                        error: function () {
                            Swal.fire
                                ({
                                    title: "OOOPS!",
                                    text: "Something Went wrong!",
                                    icon: "error"
                                });
                        }

                    })
                }
            });
    })



})