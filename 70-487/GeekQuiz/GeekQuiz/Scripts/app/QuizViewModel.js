var GeekQuiz;
(function (GeekQuiz) {
    var QuizViewModel = (function () {
        function QuizViewModel($scope, $http) {
            $scope.IsAnswered = false;
            $scope.Title = "loading question...";
            $scope.Options = [];
            $scope.IsCorrectAnswer = false;
            $scope.IsWorking = false;
            $scope.Answer = function () { return $scope.IsCorrectAnswer ? "correct" : "incorrect"; };
            $scope.NextQuestion = function () {
                $scope.IsWorking = true;
                $scope.IsAnswered = false;
                $scope.Title = "loading question...";
                $scope.Options = [];
                $http.get("/api/trivia")
                    .then(function (e) {
                    var data = e.data;
                    $scope.Options = data.options;
                    $scope.Title = data.title;
                    $scope.IsAnswered = false;
                    $scope.IsWorking = false;
                }, function () {
                    $scope.Title = "Oops...Something went wrong";
                    $scope.IsWorking = false;
                });
            };
            $scope.SendAnswer = function (o) {
                $scope.IsWorking = true;
                $scope.IsAnswered = true;
                $http.post("/api/Trivia", {
                    optionId: o.Id,
                    questionId: o.QuestionId
                })
                    .then(function (e) {
                    $scope.IsCorrectAnswer = e.data;
                    $scope.IsWorking = false;
                }, function () {
                    $scope.Title = "Oops...Something went wrong";
                    $scope.IsWorking = false;
                });
            };
        }
        return QuizViewModel;
    }());
    GeekQuiz.QuizViewModel = QuizViewModel;
})(GeekQuiz || (GeekQuiz = {}));
