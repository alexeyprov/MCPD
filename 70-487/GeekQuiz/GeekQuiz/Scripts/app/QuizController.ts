module GeekQuiz {
    export class QuizController {
        constructor($scope: IQuestionScope, $http: ng.IHttpService) {
            $scope.IsAnswered = false;
            $scope.Title = "loading question...";
            $scope.Options = [];
            $scope.IsCorrectAnswer = false;
            $scope.IsWorking = false;

            $scope.Answer = () => $scope.IsCorrectAnswer ? "correct" : "incorrect";

            $scope.NextQuestion = function () {

                $scope.IsWorking = true;
                $scope.IsAnswered = false;
                $scope.Title = "loading question...";
                $scope.Options = [];

                $http.get("/api/trivia")
                    .then(
                    e => {
                        var data: any = e.data;
                        $scope.Options = data.options;
                        $scope.Title = data.title;
                        $scope.IsAnswered = false;
                        $scope.IsWorking = false;
                    },
                    () => {
                        $scope.Title = "Oops...Something went wrong";
                        $scope.IsWorking = false;
                    });
            };

            $scope.SendAnswer = function (o: ITriviaOption) {
                $scope.IsWorking = true;
                $scope.IsAnswered = true

                $http.post<boolean>(
                    "/api/Trivia",
                    {
                        optionId: o.id,
                        questionId: o.questionId
                    })
                    .then(
                    e => {
                        $scope.IsCorrectAnswer = e.data;
                        $scope.IsWorking = false;
                    },
                    () => {
                        $scope.Title = "Oops...Something went wrong";
                        $scope.IsWorking = false;
                    });
            };
        }
    }
}