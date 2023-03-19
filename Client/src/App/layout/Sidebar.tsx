import './Sidebar.css'
import { Link } from 'react-router-dom'
import React, { useState } from 'react'

interface LayoutProps {
  children: React.ReactNode
}

interface SidebarProps {
  isOpen: boolean
  toggleSidebar: () => void
}

export default function Sidebar({
  isOpen,
  toggleSidebar,
  children,
}: SidebarProps & LayoutProps) {
  const isActive = (path: string) => window.location.pathname === path

  return (
    <>
      <div
        className={`sidebar z-10 text-white mt-16 bg-gray-800`}
        onMouseEnter={toggleSidebar}
        onMouseLeave={toggleSidebar}
      >
        <div className="sidebar-icon">
          <Link
            to="/movies"
            className={`hover:bg-gray-900 w-full py-2 pl-3 flex items-center ${
              isActive('/movies') ? 'active' : ''
            }`}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="16"
              height="16"
              fill="currentColor"
              className={`bi bi-film mr-2 ${
                isActive('/movies') && 'text-red-600'
              }`}
              viewBox="0 0 16 16"
            >
              <path d="M0 1a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v14a1 1 0 0 1-1 1H1a1 1 0 0 1-1-1V1zm4 0v6h8V1H4zm8 8H4v6h8V9zM1 1v2h2V1H1zm2 3H1v2h2V4zM1 7v2h2V7H1zm2 3H1v2h2v-2zm-2 3v2h2v-2H1zM15 1h-2v2h2V1zm-2 3v2h2V4h-2zm2 3h-2v2h2V7zm-2 3v2h2v-2h-2zm2 3h-2v2h2v-2z" />
            </svg>{' '}
            <p className="titleUrl">Movies </p>
            {isActive('/movies') && (
              <div className="ml-auto w-1 h-5 rounded-l-lg bg-red-600" />
            )}
          </Link>

          <Link
            to="/movieRoom"
            className={`hover:bg-gray-900 w-full py-2 pl-3 flex items-center ${
              isActive('/movieRoom') ? 'active' : ''
            }`}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="16"
              height="16"
              fill="currentColor"
              className={`bi bi-camera-reels mr-2 ${
                isActive('/movieRoom') && 'text-red-600'
              }`}
              viewBox="0 0 16 16"
            >
              <path d="M6 3a3 3 0 1 1-6 0 3 3 0 0 1 6 0zM1 3a2 2 0 1 0 4 0 2 2 0 0 0-4 0z" />
              <path d="M9 6h.5a2 2 0 0 1 1.983 1.738l3.11-1.382A1 1 0 0 1 16 7.269v7.462a1 1 0 0 1-1.406.913l-3.111-1.382A2 2 0 0 1 9.5 16H2a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h7zm6 8.73V7.27l-3.5 1.555v4.35l3.5 1.556zM1 8v6a1 1 0 0 0 1 1h7.5a1 1 0 0 0 1-1V8a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1z" />
              <path d="M9 6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zM7 3a2 2 0 1 1 4 0 2 2 0 0 1-4 0z" />
            </svg>{' '}
            <p className="titleUrl">Movie Room </p>
            {isActive('/movieRoom') && (
              <div className="ml-auto w-1 h-5 rounded-l-lg bg-red-600" />
            )}
          </Link>
        </div>
      </div>
      <div className={`content ${isOpen ? 'shift' : ''}`}>{children}</div>
    </>
  )
}
