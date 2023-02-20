import { Button } from '@/_components';
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { articleService } from '@/_services';

function List({ match }) {
    const { path } = match;
    const [articles, setArticles] = useState(null);

    useEffect(() => {
        articleService.getAll().then(x => setArticles(x));
    }, []);

    function deleteArticle(id) {
        setArticles(articles.map(x => {
            if (x.id === id) { x.isDeleting = true; }
            return x;
        }));
        articleService.delete(id).then(() => {
            setArticles(articles => articles.filter(x => x.id !== id));
        });
    }

    return (
        <div className="container m-auto max-w-4xl">
            <h1 className="text-3xl font-bold mb-12">Articles</h1>
            <p className="flex justify-end mb-10">
                <Button text="Add Article" to={`${path}/add`} />
            </p>
            <div className="grid grid-cols-3 gap-2 mx-auto">
                {articles && articles.map(article =>
                    <div
                        key={article.id}
                        className="max-w-sm"
                    >
                        <Link to={`${path}/edit/${article.id}`}>
                            <div className="max-w-sm mx-auto grid grid-cols-1" key={article.id}>
                                <div className="relative p-3 col-start-1 row-start-1 flex flex-col-reverse rounded-lg bg-gradient-to-t from-black/75 via-black/0">
                                    <h1 className="mt-1 text-lg font-semibold text-white">{article.title}</h1>
                                    <p className="text-sm leading-4 font-medium text-white">{article.type}</p>
                                </div>
                                <div className="grid gap-4 col-start-1 col-end-3 row-start-1">
                                    <img src={article.imageUrl} alt="" className="w-full h-60 object-cover rounded-lg h-52 col-span-full" loading="lazy" />
                                </div>
                            </div>
                        </Link>
                    </div>
                )}
            </div>
        </div>
    );
}

export { List };