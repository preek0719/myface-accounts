import React, {useEffect, useState, useContext} from 'react';
import {ListResponse, Post} from "../../Api/apiClient";
import {Grid} from "../Grid/Grid";
import {PostCard} from "../PostCard/PostCard";
import {LoginContext} from "../../Components/LoginManager/LoginManager";

interface PostListProps {
    title: string,
    fetchPosts: (authHeader: string) => Promise<ListResponse<Post>>
}

export function PostList(props: PostListProps): JSX.Element {
    const [posts, setPosts] = useState<Post[]>([]);
    const {authHeader} = useContext(LoginContext);
    
    
    useEffect(() => {
        if (authHeader){
            props.fetchPosts(authHeader)
            .then(response => setPosts(response.items))
            .catch(err => console.error(err));
        }
        // props.fetchPosts(authHeader)
        //     .then(response => setPosts(response.items));
        
    }, [authHeader, props]);

    
    return (
        <section>
            <h2>{props.title}</h2>
            <Grid>
                {posts.map(post => <PostCard key={post.id} post={post}/>)}
            </Grid>
        </section>
    );
}