module GeekQuiz {
    export interface IQuestionScope extends ng.IScope {
        IsAnswered: boolean;

        Title: string;

        Options: ITriviaOption[];

        IsCorrectAnswer: boolean;

        IsWorking: boolean;

        Answer: () => string;

        NextQuestion: () => void;

        SendAnswer: (o: ITriviaOption) => void;
    }
}