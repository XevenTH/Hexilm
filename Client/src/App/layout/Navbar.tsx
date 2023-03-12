import { useState } from 'react';
import { UseStore } from '../Stores/BaseStore';

function Navbar() {
    const { UserStore: { User, logout } } = UseStore();

    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [isOpen, setIsOpen] = useState(false);

    const toggleDropdown = () => {
        setIsOpen(!isOpen);
    };

    return (
        <nav  className="relative z-10 backdrop-blur-lg">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">
                    <div className="flex items-center">
                        <a href="#" className="bg-gradient-to-r from-sky-400 via-purple-500 to-fuchsia-600 bg-clip-text text-transparent text-2xl font-bold">
                            CoolMovie
                        </a>
                    </div>
                    <div className="hidden sm:block">
                        <div className="relative">
                            <button
                                className="inline-flex items-center justify-center w-full px-4 py-2 text-md font-medium text-gray-700 bg-slate-100 rounded-md hover:bg-slate-300 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-slate-300"
                                onClick={toggleDropdown}
                            >
                                {User ?
                                    User?.displayname : "Displayname"}
                                <div className="ml-2 w-6 h-6 bg-black rounded-full"></div>
                            </button>
                            {isOpen && (
                                <div className="absolute right-0 w-56 mt-2 origin-top-right bg-slate-100 rounded-md shadow-lg z-10 text" onClick={() => setIsOpen(false)}>
                                    <div className="py-1" role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                                        <a href="#" className="block px-4 py-2 text-md font-semibold text-gray-700 hover:bg-slate-300 hover:text-gray-900 z-10" role="menuitem">
                                            Profile
                                        </a>
                                        <a className="block px-4 py-2 text-md font-semibold text-gray-700 hover:bg-slate-300 hover:text-gray-900 z-10" role="menuitem" onClick={(event) => {event.preventDefault(); logout()}}>
                                            Logout
                                        </a>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>
                    <div className="-mr-2 flex sm:hidden">
                        <button
                            onClick={() => setIsMenuOpen(!isMenuOpen)}
                            type="button"
                            className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
                            aria-controls="mobile-menu"
                            aria-expanded="false"
                        >
                            <span className="sr-only">Open main menu</span>
                            {/* Icon when menu is closed. */}
                            <svg className={`${isMenuOpen ? 'hidden' : 'block'} h-6 w-6`} stroke="currentColor" fill="none" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                            </svg>
                            {/* Icon when menu is open. */}
                            <svg className={`${isMenuOpen ? 'block' : 'hidden'} h-6 w-6`} stroke="currentColor" fill="none" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>
                </div>
            </div>

            {/* Mobile menu, toggle classes based on menu state. */}
            <div className={`${isMenuOpen ? 'block' : 'hidden'} sm:hidden`} id="mobile-menu">
                <div className="absolute right-0 w-56 mt-2 origin-top-right bg-white rounded-md shadow-lg z-10 text" onClick={() => setIsOpen(false)}>
                    <div className="py-1" role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                        <a href="#" className="block px-4 py-2 text-md font-semibold text-gray-700 hover:bg-gray-100 hover:text-gray-900 z-10" role="menuitem">
                            Profile
                        </a>
                        <button className="block px-4 py-2 text-md font-semibold text-gray-700 hover:bg-gray-100 hover:text-gray-900" role="menuitem" onClick={() => logout()}>
                            Logout
                        </button>
                    </div>

                </div>
            </div>
        </nav >
    );
}

export default Navbar;
