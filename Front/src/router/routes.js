const routes = [
  {
    path: '/',
    component: () => import('pages/Login-1.vue')
  },
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {path: '/Dashboard', component: () => import('pages/Dashboard.vue')},
      {path: '/Profile', component: () => import('pages/UserProfile.vue')},
      {path: '/UserList', component: () => import('src/pages/UserList.vue')},
      {path: '/RoleList', component: () => import('src/pages/RoleList.vue')},
      {path: '/UserLoginLogList/:id', component: () => import('src/pages/UserLoginLogList.vue')},
      {path: '/CreateRole/:id?', component: () => import('pages/CreateRole.vue')},
      {path: '/CreateUser/:id?', component: () => import('pages/CreateUser.vue')},


      // Not completed yet
      // {path: '/Taskboard', component: () => import('pages/TaskBoard.vue')},
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/Error404.vue')
  }
]

export default routes
