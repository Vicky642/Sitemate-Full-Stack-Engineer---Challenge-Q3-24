// issues.js

// ViewModel for managing issues
function IssuesViewModel() {
    var self = this;

    // Observable properties
    self.newIssueTitle = ko.observable('');
    self.newIssueDescription = ko.observable('');
    self.issues = ko.observableArray([]);

    // Fetch issues from the server
    self.fetchIssues = function () {
        $.ajax({
            url: '/Sitemate',
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
            url: '/Sitemate',
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
        $.ajax({
            url: '/Sitemate/' + issue.id,
            method: 'DELETE',
            success: function () {
                self.fetchIssues();
            },
            error: function (error) {
                console.error('Error deleting issue:', error);
            }
        });
    };

    // Initial fetch of issues
    self.fetchIssues();
}

// Apply bindings
$(document).ready(function () {
    ko.applyBindings(new IssuesViewModel());
});
