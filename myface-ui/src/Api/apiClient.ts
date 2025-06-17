export interface ListResponse<T> {
    items: T[];
    totalNumberOfItems: number;
    page: number;
    nextPage: string;
    previousPage: string;
}

export interface User {
    id: number;
    firstName: string;
    lastName: string;
    displayName: string;
    username: string;
    email: string;
    profileImageUrl: string;
    coverImageUrl: string;
    hashedPassword: string;
    salt: any;
}

export interface Interaction {
    id: number;
    user: User;
    type: string;
    date: string;
}

export interface Post {
    id: number;
    message: string;
    imageUrl: string;
    postedAt: string;
    postedBy: User;
    likes: Interaction[];
    dislikes: Interaction[];
}

export interface NewPost {
    message: string;
    imageUrl: string;
    userId: number;
    
}

export async function fetchUsers(authHeader:string, searchTerm: string, page: number, pageSize: number): Promise<ListResponse<User>> {
    const response = await fetch(`https://localhost:5001/users?search=${searchTerm}&page=${page}&pageSize=${pageSize}`, { headers: {"Authorization": `${authHeader}`}});
    return await response.json();
} //authHeaer in component

export async function fetchUser(authHeader:string, userId: string | number): Promise<User> {
    const response = await fetch(`https://localhost:5001/users/${userId}`, { headers: {"Authorization": `${authHeader}`}});
    
    return await response.json();
} //authHeaer in component

export async function fetchPosts(authHeader:string, page: number, pageSize: number, ): Promise<ListResponse<Post>> {
    const response = await fetch(`https://localhost:5001/feed?page=${page}&pageSize=${pageSize}`, { headers: {"Authorization": `${authHeader}`}});
    return await response.json();
}//authHeaer in component

export async function fetchPostsForUser(authHeader:string, page: number, pageSize: number, userId: string | number) {
    const response = await fetch(`https://localhost:5001/feed?page=${page}&pageSize=${pageSize}&postedBy=${userId}`, { headers: {"Authorization": `${authHeader}`}});
    return await response.json();
} 

export async function fetchPostsLikedBy(authHeader:string, page: number, pageSize: number, userId: string | number) {
    const response = await fetch(`https://localhost:5001/feed?page=${page}&pageSize=${pageSize}&likedBy=${userId}`, { headers: {"Authorization": `${authHeader}`}});
    return await response.json();
}

export async function fetchPostsDislikedBy(authHeader:string, page: number, pageSize: number, userId: string | number) {
    const response = await fetch(`https://localhost:5001/feed?page=${page}&pageSize=${pageSize}&dislikedBy=${userId}`, { headers: {"Authorization": `${authHeader}`}});
    return await response.json();
}

export async function createPost(authHeader:string, newPost: NewPost) {
    const response = await fetch(`https://localhost:5001/posts/create`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `${authHeader}`
        },
        body: JSON.stringify(newPost), 
    });
    
    if (!response.ok) {
        throw new Error(await response.json())
    }
}
