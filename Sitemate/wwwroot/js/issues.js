// issues.js

// ViewModel for managing issues
function IssuesViewModel() {
    var self = this;

    // Observable properties
    self.newIssueTitle = ko.observable('');
    self.newIssueDescription = ko.observable('');
    self.issues = ko.observableArray([]);
    self.selectedIssue = ko.observable(null); // To store the issue being edited
    self.searchId = ko.observable(''); // To store the ID for searching

    // Fetch issues from the server
    self.fetchIssues = function () {
        $.ajax({
            url: '/Sitemate/GetAll',
            method: 'GET',
            success: function (data) {
                self.issues(data);
            },
            error: function (error) {
                console.error('Error fetching issues:', error);
            }
        });
    };

    // Add a new issue
    self.addIssue = function () {
        var newIssue = {
            title: self.newIssueTitle(),
            description: self.newIssueDescription()
        };

        $.ajax({
            url: '/Sitemate/Add',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newIssue),
            success: function () {
                self.fetchIssues();
                self.newIssueTitle('');
                self.newIssueDescription('');
            },
            error: function (error) {
                console.error('Error adding issue:', error);
            }
        });
    };

    // Delete an issue
    self.deleteIssue = function (issue) {
        debugger;
        $.ajax({
            url: '/Sitemate/Delete/' + issue.id,
            method: 'DELETE',
            success: function () {
                self.fetchIssues();
            },
            error: function (error) {
                console.error('Error deleting issue:', error);
            }
        });
    };

    // Update an existing issue
    self.updateIssue = function () {
        var issue = self.selectedIssue();
        if (issue) {
            var updatedIssue = {
                id: issue.id,
                title: self.newIssueTitle(),
                description: self.newIssueDescription()
            };

            $.ajax({
                url: '/Sitemate/Update/' + issue.id,
                method: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(updatedIssue),
                success: function () {
                    self.fetchIssues();
                    self.newIssueTitle('');
                    self.newIssueDescription('');
                    self.selectedIssue(null); // Clear the selected issue after updating
                },
                error: function (error) {
                    console.error('Error updating issue:', error);
                }
            });
        }
    };


    // Search for an issue by ID
    self.searchIssueById = function () {
        var id = self.searchId();
        if (id) {
            $.ajax({
                url: '/Sitemate/GetById/' + id,
                method: 'GET',
                success: function (data) {
                    self.selectedIssue(data);
                    self.newIssueTitle(data.title);
                    self.newIssueDescription(data.description);
                },
                error: function (error) {
                    console.error('Error searching issue by ID:', error);
                }
            });
        }
    };

    // Initial fetch of issues
    self.fetchIssues();
}

// Apply bindings
$(document).ready(function () {
    ko.applyBindings(new IssuesViewModel());
});
