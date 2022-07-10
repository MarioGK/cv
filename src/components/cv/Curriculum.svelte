<script lang='ts'>
  import { onMount } from 'svelte'

  import type { 
    CurriculumType, CurriculumProfileType, CurriculumCareerType, 
    CurriculumPositionType, CurriculumSkillAreaType, CurriculumOtherSkillsType, 
    CurriculumEducationTitleType, CurriculumAwardType, CurriculumExtraType, CurriculumProjectType 
  } from './Curriculum.type'

  import Profile from './Profile.svelte'
  import Career from './Career.svelte'
  import WorkExperience from './WorkExperience.svelte'
  import Skills from './Skills.svelte'
  import Education from './Education.svelte'
  import Awards from './Awards.svelte'
  import Extras from './Extras.svelte'
  import Projects from './Projects.svelte'

  export let info: CurriculumType | null = null
  export let src: string | null = null

  let 
    profile:      CurriculumProfileType, 
    career:       CurriculumCareerType,
    positions:    CurriculumPositionType[] | null,
    skillAreas:   CurriculumSkillAreaType[] | null,
    otherSkills:  CurriculumOtherSkillsType,
    education:    CurriculumEducationTitleType[] | null,
    awards:       CurriculumAwardType[] | null,
    extras:       CurriculumExtraType[] | null,
    projects:     CurriculumProjectType[] | null

  $: if (info) {
    ({ profile, career, positions, skillAreas, otherSkills, education, awards, extras, projects } = info)
  }
  onMount(async () => {
    if (src && !info) info = await (await fetch(src)).json()      // fetch from src
  })

</script>

{#if info}
<article class="resume-wrapper text-center position-relative">

  <div class="resume-wrapper-inner mx-auto text-left bg-white shadow-lg">
    <Profile {profile} />

    <div class="resume-body p-5">
      <Career {career} />

      <div class="row">

        <div class="col-lg-9">
          <Projects {projects} />
          <WorkExperience {positions} />
        </div>

        <div class="col-lg-3">
          <Skills {skillAreas} {otherSkills} />

          <Education {education} />

          <Awards {awards} />

          <Extras {extras} />
        </div>
      </div><!--//row-->

    </div><!--//resume-body-->

  </div>
</article>  


<footer class="footer text-center pt-2 pb-5">
  <!--/* This template is free as long as you keep the footer attribution link. If you'd like to use the template without the attribution link, you can buy the commercial license via our website: themes.3rdwavemedia.com Thank you for your support. :) */-->
</footer>
{:else}
<p>Loading...</p>
{/if}