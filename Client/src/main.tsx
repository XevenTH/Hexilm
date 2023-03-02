import ReactDOM from 'react-dom/client'
import { RouterProvider } from 'react-router-dom'
import { browserRoute } from './App/Router/Route'
import { BaseStoreContext, storeContainer } from './App/Stores/BaseStore'
import './styles.css'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement)
    .render(
        <BaseStoreContext.Provider value={storeContainer}>
            <RouterProvider router={browserRoute} />
        </BaseStoreContext.Provider>
    )
