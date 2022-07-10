import type { CurriculumType } from '../components/cv/Curriculum.type'

export const info: CurriculumType = {
  profile: {
    name: 'Mario Gabriell Karaziaki Belchior',
    title: 'Full Stack Developer',
    img: 'assets/images/profile/1.jpg',
    email: 'mariogk01@gmail.com',
    cell: '+55 44 999758367',
    social: [
      { icon: 'fa-stack-overflow', url: 'https://stackoverflow.com/users/8093775/m%c3%a1rio-gabriel', text: 'StackOverflow' },
      { icon: 'fa-github-alt', url: '//github.com/mariogk', "text": 'GitHub' },
      { icon: 'fa-gitlab', url: '//gitlab.com/mariogk', "text": 'GitLab' },
      { icon: 'fa-linkedin-in', url: '//linkedin.com/in/m%C3%A1rio-gabriell-karaziaki-belchior-0a271814b/', "text": 'LinkedIn' },
    ]
  },
  career: {
    title: 'career summary',
    summary: `
Full Stack Developer with 6+ years of hands-on experience developing, and implementing applications and solutions using a wide range of technologies and programming languages.
    `
  },
  skillAreas: [
    {
      title: 'Frontend',
      skills: [
        { title: 'Blazor', experience: 100 },
        { title: 'TypeScript', experience: 50 },
        { title: 'Svelte', experience: 40 },
        { title: 'JavaScript/ES6', experience: 40 },
        { title: 'Django', experience: 20 },
      ]
    },
    {
      title: 'Backend',
      skills: [
        { title: 'C#/.NET Core', experience: 100 },
        { title: 'DevOps', experience: 70 },
        { title: 'CI', experience: 70 },
        { title: 'Node.js', experience: 40 },
        { title: 'Python', experience: 20 },
      ]
    },
    {
      title: 'Databases',
      skills: [
        { title: 'PostgreSQL', experience: 100 },
        { title: 'MySQL', experience: 80 },
        { title: 'RavenDB', experience: 60 },
      ]
    },
  ],
  otherSkills: 'git;AWS;Docker; Kubernetes; Docker Swarm;HTML;CSS;Bootstrap',
  education: [
    {
      degree: 'Engineering Control And Automation Degree',
      organization: 'Unisociesc - Brazil',
      time: '2017 - 2022',
    },
  ],
  awards: [
  ],
  extras: [
    {
      title: 'Language',
      items: [
        { title: 'Portuguese', comment: '(Native)', text: '' },
        { title: 'English', comment: '(Professional)', text: 'Good writing and speaking skills' },
        { title: 'Spanish', text: 'Reading skills' },
      ]
    },
    {
      title: 'Other Interests',
      items: [
        { title: 'IOT', comment: '', text: '' },
        { title: '3D Printing', comment: '', text: '' },
      ]
    },
  ],

}