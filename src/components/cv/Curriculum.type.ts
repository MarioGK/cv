export type CurriculumType = {
  profile: CurriculumProfileType,
  career: CurriculumCareerType,
  positions?: CurriculumPositionType[]
  skillAreas?: CurriculumSkillAreaType[],
  otherSkills: CurriculumOtherSkillsType,
  education?: CurriculumEducationTitleType[],
  awards?: CurriculumAwardType[],
  extras?: CurriculumExtraType[],
  projects?: CurriculumProjectType[]
}

export type CurriculumProfileType = {
  name: string,
  title: string,
  img: string,
  email: string,
  cell: string,
  social: CurriculumSocialType[],
}

export type CurriculumSocialType = {
  icon?: string, 
  url: string, 
  text?: string,
}

export type CurriculumCareerType = {
  title?: string,
  summary: string,
}

export type CurriculumPositionType = {
  title?: string,
  company?: string,
  time?: string,
  text?: string,
  sections?: CurriculumListItemType[],
  technologies?: string | string[]
}

export type CurriculumListItemType = {
  title?: string,
  text?: string,
  items?: string | string[]
}

export type CurriculumSkillAreaType = {
  title: string,
  skills: CurriculumSkillType[]
}

export type CurriculumSkillType = {
  title: string,
  experience: number,
}

export type CurriculumOtherSkillsType = string | string[]

export type CurriculumEducationTitleType = {
  degree: string,
  organization: string,
  time: string,
} 

export type CurriculumAwardType = {
  title: string,
  time?: string,
  description?: string,
}

export type CurriculumExtraType = {
  title: string,
  items: CurriculumExtraItemType[]
  
} 

export type CurriculumExtraItemType = {
  title?: string,
  comment?: string,
  text?: string
} 

export type CurriculumProjectType = {
  title?: string,
  company?: string,
  time?: string,
  text?: string,
  sections?: CurriculumListItemType[],
  technologies?: string | string[],
  images?: CurriculumImageType[],
}

export type CurriculumImageType = {
  src: string,
  href?: string,
  title?: string,
  alt?: string,
  text?: string,
}
