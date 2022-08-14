export interface Challenge {
    id: number;
    name: string;
    description: string;
    completed: boolean;
    started: boolean;
    threshold: number;
}