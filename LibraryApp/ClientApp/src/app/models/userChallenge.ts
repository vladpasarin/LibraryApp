export interface UserChallenge {
    id: number;
    userId: number;
    challengeId: number;
    progress: number;
    threshold: number;
    completed: boolean;
}