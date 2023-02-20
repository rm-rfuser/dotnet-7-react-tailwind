import React, { useEffect } from 'react';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

import { Button, Label, SubmitButton } from '@/_components';
import { articleService } from '@/_services';

function AddEdit({ history, match }) {
    const { id } = match.params;
    const isAddMode = !id;
    
    const validationSchema = Yup.object().shape({
        title: Yup.string()
            .required('Title is required'),
        description: Yup.string()
            .required('Description is required'),
        imageUrl: Yup.string()
            .url('URL is invalid')
            .required('Image URL is required'),
        type: Yup.string()
            .required('Type is required'),
    });

    const { register, handleSubmit, reset, setValue, errors } = useForm({
        resolver: yupResolver(validationSchema)
    });

    function onSubmit(data) {
        return isAddMode
            ? createArticle(data)
            : updateArticle(id, data);
    }

    function createArticle(data) {
        return articleService.create(data)
            .then(() => {
                history.push('.');
            })
            .catch(alertService.error);
    }

    function updateArticle(id, data) {
        return articleService.update(id, data)
            .then(() => {
                history.push('..');
            })
            .catch(alertService.error);
    }

    useEffect(() => {
        if (!isAddMode) {
            articleService.getById(id).then(article => {
                const fields = ['title', 'description', 'imageUrl', 'type'];
                fields.forEach(field => setValue(field, article[field]));
            });
        }
    }, []);

    return (
        <div className="container m-auto max-w-xl">
            <h1 className="text-3xl font-bold mb-12">{isAddMode ? 'Add Article' : 'Edit Article'}</h1>
            <form
                onSubmit={handleSubmit(onSubmit)}
                onReset={reset}
            >
                <div className="container">
                    <div>
                        <Label text="Title" />
                        <input name="title" type="text" ref={register} className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 ${errors.title ? 'border-red-500' : ''}`} />
                        <div className="invalid-feedback">{errors.title?.message}</div>
                    </div>
                    <div className="mt-5">
                        <Label text="Description" />
                        <input name="description" type="text" ref={register} className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 ${errors.description ? 'border-red-500' : ''}`} />
                        <div className="invalid-feedback">{errors.description?.message}</div>
                    </div>
                    <div className="mt-5">
                        <Label text="Image URL" />
                        <input name="imageUrl" type="text" ref={register} className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 ${errors.imageUrl ? 'border-red-500' : ''}`} />
                        <div className="invalid-feedback">{errors.imageUrl?.message}</div>
                    </div>
                    <div className="mt-5">
                        <Label text="Type" />
                        <select name="type" ref={register} className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 ${errors.type ? 'border-red-500' : ''}`}>
                            <option value=""></option>
                            <option value="News">News</option>
                            <option value="Event">Event</option>
                        </select>
                        <div className="invalid-feedback">{errors.type?.message}</div>
                    </div>
                </div>
                <div className="mt-10 flex justify-end">
                    <SubmitButton text="Save" />
                    <Button text="Cancel" to={isAddMode ? '.' : '..'} />
                </div>
            </form>
        </div>
    );
}

export { AddEdit };